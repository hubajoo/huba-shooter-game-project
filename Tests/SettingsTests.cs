using NUnit.Framework;
using System;
using System.IO;
using Newtonsoft.Json;
using DungeonCrawl.GameSetup;

namespace Tests
{
  public class SettingsTests
  {
    private const string ValidSettingsPath = "Data/settings.txt";
    private const string InvalidSettingsPath = "Data/invalid_settings.txt";
    private const string MissingSettingsPath = "Data/missing_settings.txt";

    [SetUp]
    public void Setup()
    {
      // Create an invalid settings file
      File.WriteAllText(InvalidSettingsPath, "Invalid JSON content");
    }

    [TearDown]
    public void Teardown()
    {
      // Clean up the created file
      if (File.Exists(InvalidSettingsPath))
      {
        File.Delete(InvalidSettingsPath);
      }
    }


    [Test]
    public void SettingsReader_ReadSettings_ValidFile_ReturnsGameSettings()
    {
      ISettingsReader settingsReader = new SettingsReader();
      IGameSettings settings = settingsReader.ReadSettings(ValidSettingsPath);
      Assert.That(settings, Is.Not.Null);
      Assert.That(settings.ServerUrl, Is.EqualTo("http://127.0.0.1:8090"));
    }

    [Test]
    public void SettingsReader_ReadSettings_InvalidFile_ThrowsException()
    {
      ISettingsReader settingsReader = new SettingsReader();
      Assert.Throws<Exception>(() => settingsReader.ReadSettings(InvalidSettingsPath));
    }

    [Test]
    public void SettingsReader_ReadSettings_MissingFile_ThrowsException()
    {
      ISettingsReader settingsReader = new SettingsReader();
      Assert.Throws<Exception>(() => settingsReader.ReadSettings(MissingSettingsPath));
    }

    [Test]
    public void GameSettings_Constructor_ValidSettings_SetsProperties()
    {
      string[] lines = new string[]
      {
                "UserName=TestUser",
                "ViewPortWidth=1",
                "ViewPortHeight=1",
                "StatsConsoleWidth=1",
                "PlayerHealth=1",
                "PlayerDamage=1",
                "PlayerRange=1",
                "ServerUrl=http://testaddress.com"
      };

      GameSettings settings = new GameSettings(lines);

      Assert.That(settings.UserName, Is.EqualTo("TestUser"));
      Assert.That(settings.ViewPortWidth, Is.EqualTo(1));
      Assert.That(settings.ViewPortHeight, Is.EqualTo(1));
      Assert.That(settings.StatsConsoleWidth, Is.EqualTo(1));
      Assert.That(settings.PlayerHealth, Is.EqualTo(1));
      Assert.That(settings.PlayerDamage, Is.EqualTo(1));
      Assert.That(settings.PlayerRange, Is.EqualTo(1));
      Assert.That(settings.ServerUrl, Is.EqualTo("http://testaddress.com"));
    }

    [Test]
    public void GameSettings_Constructor_InvalidFormat_ThrowsException()
    {
      string[] lines = new string[]
      {
                "UserName=TestUser",
                "InvalidLineWithoutEqualsSign"
      };

      var ex = Assert.Throws<Exception>(() => new GameSettings(lines));
      Assert.That(ex.Message, Is.EqualTo("Invalid settings file format"));
    }

    [Test]
    public void GameSettings_Constructor_MissingRequiredSetting_ThrowsException()
    {
      string[] lines = new string[]
      {
                "UserName=TestUser",
                //Missing ViewPortWidth,
                "ViewPortHeight=600",
                "StatsConsoleWidth=200",
                "PlayerHealth=100",
                "PlayerDamage=10",
                "PlayerRange=5",
                "ServerUrl=http://testaddress.com"
      };
      var ex = Assert.Throws<Exception>(() => new GameSettings(lines));
      Assert.That(ex.Message, Is.EqualTo("The given key 'ViewPortWidth' was not present in the dictionary."));
    }

    [Test]
    public void GameSettings_Constructor_InvalidNumberFormat_ThrowsException()
    {
      string[] lines = new string[]
      {
                "UserName=TestUser",
                "ViewPortWidth=800",
                "ViewPortHeight=600",
                "StatsConsoleWidth=200",
                "PlayerHealth=100",
                "PlayerDamage=InvalidNumber",
                "PlayerRange=5",
                "ServerUrl=http://127.0.0.1:8090"
      };

      var ex = Assert.Throws<Exception>(() => new GameSettings(lines));
      Assert.That(ex.Message, Is.EqualTo("The input string 'InvalidNumber' was not in a correct format."));
    }

    [Test]
    public void DefaultSettings_Constructor_InitializesDefaultSettings()
    {
      DefaultSettings defaultSettings = new DefaultSettings();
      IGameSettings settings = defaultSettings.Settings;

      ///Default settings
      ///StatsConsoleWidth:25
      ///ViewPortWidth:100
      ///ViewPortHeight:40
      ///PlayerHealth:100
      ///PlayerDamage:1
      ///PlayerRange:5
      ///UserName:Bob
      ///ServerUrl:https://localhost:8090

      Assert.That(settings, Is.Not.Null);
      Assert.That(settings.UserName, Is.EqualTo("Bob"));
      Assert.That(settings.ViewPortWidth, Is.EqualTo(100));
      Assert.That(settings.ViewPortHeight, Is.EqualTo(40));
      Assert.That(settings.StatsConsoleWidth, Is.EqualTo(25));
      Assert.That(settings.PlayerHealth, Is.EqualTo(100));
      Assert.That(settings.PlayerDamage, Is.EqualTo(1));
      Assert.That(settings.PlayerRange, Is.EqualTo(5));
      Assert.That(settings.ServerUrl, Is.EqualTo("https://localhost:8090"));
    }
  }
}