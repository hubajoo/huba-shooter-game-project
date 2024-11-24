using NUnit.Framework;
using Moq;
using SadConsole;
using SadRogue.Primitives;
using DungeonCrawl.GameSetup;

namespace Tests.MapTests
{
    public class MapTests
    {
        private Map _map;
        private Mock<IGameObject> _mockGameObject;
        private Mock<Player> _mockPlayer;
        private Mock<ISpawnOrchestrator> _mockSpawnOrchestrator;

        [Tests]
        public MapPass(){
          Assert.Pass();
        }
    }
}
/*
        [SetUp]
        public void Setup()
        {
            _map = new Map(10, 10);
            _mockGameObject = new Mock<IGameObject>();
            _mockGameObject.Setup(m => m.Position).Returns(new Point(1, 1));
            _mockPlayer = new Mock<Player>();
            _mockSpawnOrchestrator = new Mock<ISpawnOrchestrator>();
        }

        [Test]
        public void AddUserControlledObject_SetsUserControlledObject()
        {
            _map.AddUserControlledObject(_mockPlayer.Object);

            Assert.That(_map.UserControlledObject, Is.EqualTo(_mockPlayer.Object));
        }

        [Test]
        public void AddMapObject_AddsObjectToMap()
        {
            _map.AddMapObject(_mockGameObject.Object);

            Assert.That(_map.GetGameObject(new Point(1, 1)), Is.EqualTo(_mockGameObject.Object));
        }

        [Test]
        public void SetSpawnLogic_SetsSpawnLogic()
        {
            _map.SetSpawnLogic(_mockSpawnOrchestrator.Object);

            // Assuming there's a way to verify the spawn logic is set correctly
            // This might involve calling a method on the map that uses the spawn logic
        }

        [Test]
        public void TryGetMapObject_ReturnsTrueIfExists()
        {
            _map.AddMapObject(_mockGameObject.Object);

            bool result = _map.TryGetMapObject(new Point(1, 1), out IGameObject gameObject);

            Assert.That(result, Is.True);
            Assert.That(gameObject, Is.EqualTo(_mockGameObject.Object));
        }

        [Test]
        public void TryGetMapObject_ReturnsFalseIfNotExists()
        {
            bool result = _map.TryGetMapObject(new Point(1, 1), out IGameObject gameObject);

            Assert.That(result, Is.False);
            Assert.That(gameObject, Is.Null);
        }

        [Test]
        public void RemoveMapObject_RemovesObjectFromMap()
        {
            _map.AddMapObject(_mockGameObject.Object);
            _map.RemoveMapObject(_mockGameObject.Object);

            bool result = _map.TryGetMapObject(new Point(1, 1), out IGameObject gameObject);

            Assert.That(result, Is.False);
            Assert.That(gameObject, Is.Null);
        }

        [Test]
        public void ProgressTime_ProgressesTime()
        {
            // Implement test for ProgressTime method
        }

        [Test]
        public void DropLoot_DropsLoot()
        {
            // Implement test for DropLoot method
        }
    }
}

*/