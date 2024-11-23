using NUnit.Framework;
using System;
using System.IO;
using Newtonsoft.Json;
using GameSetup;

namespace Tests
{
  public class SettingsTests
  {
    private const string ValidSettingsPath = "Game/Data/valid_settings.txt";
    private const string InvalidSettingsPath = "Game/Data/invalid_settings.txt";
    private const string MissingSettingsPath = "Game/Data/missing_settings.txt";

    [SetUp]
    public void Setup()
    {
      // Create a valid settings file
      IGameSettings validSettings = new DefaultSettings().Settings;
      File.WriteAllText(ValidSettingsPath, JsonConvert.SerializeObject(validSettings));

      // Create an invalid settings file
      File.WriteAllText(InvalidSettingsPath, "Invalid JSON content");
    }

    [TearDown]
    public void Teardown()
    {
      // Clean up the created files
      if (File.Exists(ValidSettingsPath))
      {
        File.Delete(ValidSettingsPath);
      }

      if (File.Exists(InvalidSettingsPath))
      {
        File.Delete(InvalidSettingsPath);
      }
    }

    [Test]
    public void ReadSettings_ValidFile_ReturnsGameSettings()
    {
      ISettingsReader settingsReader = new SettingsReader();
      IGameSettings settings = settingsReader.ReadSettings(ValidSettingsPath);
      Assert.That(settings, Is.Not.Null);
      Assert.That(settings.ServerUrl, Is.EqualTo("http://example.com"));
    }

    [Test]
    public void ReadSettings_InvalidFile_ThrowsJsonException()
    {
      ISettingsReader settingsReader = new SettingsReader();
      Assert.Throws<JsonException>(() => settingsReader.ReadSettings(InvalidSettingsPath));
    }

    [Test]
    public void ReadSettings_MissingFile_ThrowsFileNotFoundException()
    {
      ISettingsReader settingsReader = new SettingsReader();
      Assert.Throws<FileNotFoundException>(() => settingsReader.ReadSettings(MissingSettingsPath));
    }
  }
}