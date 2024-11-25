using DungeonCrawl.GameObjects;
using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.Mechanics.SpawnLogic;
using Moq;
using NUnit.Framework;
using SadRogue.Primitives;
using System.Collections.Generic;

namespace Tests.Mechanics.SpawnLogic
{
  [TestFixture]
  public class MonsterWaveTests
  {
    private Mock<IMap> _mockMap;
    private Mock<IScreenObjectManager> _mockScreenObjectManager;
    private Mock<IMonsterTypes> _mockMonsterTypes;
    private Mock<ISpawnScript> _mockSpawnScript;
    private MonsterWave _monsterWave;

    [SetUp]
    public void SetUp()
    {
      _mockMap = new Mock<IMap>();
      _mockScreenObjectManager = new Mock<IScreenObjectManager>();
      _mockMonsterTypes = new Mock<IMonsterTypes>();
      _mockSpawnScript = new Mock<ISpawnScript>();

      _mockMap.Setup(m => m.Width).Returns(20);
      _mockMap.Setup(m => m.Height).Returns(20);
      _mockSpawnScript.Setup(s => s.GetScript()).Returns(new int[][]
      {
                    new int[] { 1, 0, 0 },
                    new int[] { 3, 0, 0 },
                    new int[] { 0, 1, 0 },
                    new int[] { 0, 3, 0 },
                    new int[] { 0, 6, 0 },
                    new int[] { 0, 0, 1 },
                    new int[] { 0, 0, 3 }
      });

      _monsterWave = new MonsterWave(_mockMap.Object, _mockScreenObjectManager.Object, _mockMonsterTypes.Object, _mockSpawnScript.Object);
    }



    [Test]
    public void NoEnemiesLeft_SpawnsMonstersAccordingToScript()
    {
      _mockMonsterTypes.Setup(m => m.GetTypes()).Returns(new List<Func<Point, IScreenObjectManager, IMap, IGameObject>>
                {
                    (p, s, m) => new Mock<IGameObject>().Object,
                    (p, s, m) => new Mock<IGameObject>().Object,
                    (p, s, m) => new Mock<IGameObject>().Object
                });

      _monsterWave.NoEnemiesLeft();

      _mockMap.Verify(m => m.AddMapObject(It.IsAny<IGameObject>()), Times.Exactly(2)); // 1 monster and 1 portal
    }



    [Test]
    public void NoEnemiesLeft_AddsMonstersToMap()
    {
      _mockMonsterTypes.Setup(m => m.GetTypes()).Returns(new List<Func<Point, IScreenObjectManager, IMap, IGameObject>>
                {
                    (p, s, m) => new Mock<IGameObject>().Object,
                    (p, s, m) => new Mock<IGameObject>().Object,
                    (p, s, m) => new Mock<IGameObject>().Object
                });

      _monsterWave.NoEnemiesLeft();

      _mockMap.Verify(m => m.AddMapObject(It.IsAny<IGameObject>()), Times.AtLeastOnce);
    }


    [Test]
    public void ScriptedSpawn_SpawnsMonstersAccordingToWaveCode()
    {
      var position = new Point(10, 10);
      var waveCode = new int[] { 1, 2, 3 };

      _mockMonsterTypes.Setup(m => m.GetTypes()).Returns(new List<Func<Point, IScreenObjectManager, IMap, IGameObject>>
                {
                    (p, s, m) => new Mock<IGameObject>().Object,
                    (p, s, m) => new Mock<IGameObject>().Object,
                    (p, s, m) => new Mock<IGameObject>().Object
                });

      _monsterWave.GetType().GetMethod("scriptedSpawn", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
          .Invoke(_monsterWave, new object[] { position, waveCode });

      _mockMap.Verify(m => m.AddMapObject(It.IsAny<IGameObject>()), Times.Exactly(6));
    }
  }
}
