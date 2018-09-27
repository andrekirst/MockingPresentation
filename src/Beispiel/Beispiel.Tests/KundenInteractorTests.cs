using System;
using System.Collections.Generic;
using Beispiel.Implementations;
using Beispiel.Interfaces;
using Beispiel.Models;
using Xunit;
using Shouldly;
using Moq;

namespace Beispiel.Tests
{
    public class KundenInteractorTests
    {
        private readonly Mock<IKundenDatenspeicher> _mockKundenDatenspeicher;
        private readonly Mock<IProtokollierer> _mockProtokollierer;
        private readonly KundenInteractor _systemUnderTest;

        public KundenInteractorTests()
        {
            _mockKundenDatenspeicher = new Mock<IKundenDatenspeicher>();
            _mockProtokollierer = new Mock<IProtokollierer>();

            _systemUnderTest = new KundenInteractor(
                kundenDatenspeicher: _mockKundenDatenspeicher.Object,
                protokollierer: _mockProtokollierer.Object);
        }

        [Fact]
        public void SucheKunden_Keine_Datensaetze_Erwarte_leere_Liste_und_Infoprotokoll()
        {
            // Arrange
            _mockKundenDatenspeicher
                .Setup(expression: m => m.SucheKunden(It.IsAny<string>()))
                .Returns(value: new List<Kunde>());

            // Act
            List<Kunde> actual = _systemUnderTest.SucheKunden(filter: null);

            // Assert
            Assert.NotNull(@object: actual);
            actual.ShouldBeEmpty();

            _mockProtokollierer
                .Verify(expression: v => v.ProtokolliereInformation("Keine Kunden gefunden"), times: Times.Once);
        }

        [Fact]
        public void SucheKunden_3_Datensaetze_Erwarte_Liste_mit_3_Eintraegen_und_Infoprotokoll()
        {
            // Arrange
            _mockKundenDatenspeicher
                .Setup(expression: m => m.SucheKunden(It.IsAny<string>()))
                .Returns(value: new List<Kunde>
                {
                    new Kunde { Id = "1", Name = "Name1"},
                    new Kunde { Id = "2", Name = "Name2"},
                    new Kunde { Id = "3", Name = "Name3"}
                });

            // Act
            List<Kunde> actual = _systemUnderTest.SucheKunden(filter: null);

            // Assert
            Assert.NotNull(@object: actual);
            actual.Count.ShouldBe(expected: 3);

            _mockProtokollierer
                .Verify(expression: v => v.ProtokolliereInformation("3 Kunden gefunden"), times: Times.Once);
        }

        [Fact]
        public void SucheKunden_Fehler_aufgetreten_Erwarte_leere_Liste_und_Fehlerprotokoll()
        {
            // Arrange
            _mockKundenDatenspeicher
                .Setup(expression: m => m.SucheKunden(It.IsAny<string>()))
                .Throws<Exception>();

            // Act
            List<Kunde> actual = _systemUnderTest.SucheKunden(filter: null);

            // Assert
            Assert.NotNull(@object: actual);
            actual.ShouldBeEmpty();

            _mockProtokollierer
                .Verify(expression: v => v.ProtokolliereFehler("Es ist ein Fehler aufgetreten"), times: Times.Once);
        }
    }
}
