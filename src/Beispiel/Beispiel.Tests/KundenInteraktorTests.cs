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
    public class KundenInteraktorTests
    {
        private readonly Mock<IKundenDatenspeicher> _mockKundenDatenspeicher;
        private readonly Mock<IProtokollierer> _mockProtokollierer;
        private readonly KundenInteraktor _systemUnderTest;

        public KundenInteraktorTests()
        {
            _mockKundenDatenspeicher = new Mock<IKundenDatenspeicher>();
            _mockProtokollierer = new Mock<IProtokollierer>();

            _systemUnderTest = new KundenInteraktor(
                kundenDatenspeicher: _mockKundenDatenspeicher.Object,
                protokollierer: _mockProtokollierer.Object);
        }

        [Fact(DisplayName = "Datenspeicher gibt 0 Datensaetze zurück. Erwarte leere Liste und protokolliere Meldung \"Keine Kunden gefunden\".")]
        public void Datenspeicher_gibt_0_Datensaetze_zurueck_Erwarte_leere_Liste_und_protokolliere_Meldung()
        {
            // Arrange
            _mockKundenDatenspeicher
                .Setup(expression: m => m.SucheKunden(It.IsAny<string>()))
                .Returns(value: new List<Kunde>());

            // Act
            List<Kunde> actual = _systemUnderTest.SucheKunden(filter: null);

            // Assert
            actual.ShouldNotBeNull();
            actual.ShouldBeEmpty();

            _mockProtokollierer.Verify(
                expression: v => v.ProtokolliereInformation("Keine Kunden gefunden"),
                times: Times.Once);
        }

        [Fact(DisplayName = "Datenspeicher gibt 2 Datensaetze zurück. Erwarte Liste mit 2 Datensätzen und protokolliere Meldung \"2 Kunden gefunden\".")]
        public void Datenspeicher_gibt_2_Datensaetze_zurueck_Erwarte_Liste_mit_2_Datensaetzen_und_protokolliere_Meldung()
        {
            // Arrange
            _mockKundenDatenspeicher
                .Setup(expression: m => m.SucheKunden(It.IsAny<string>()))
                .Returns(value: new List<Kunde>
                {
                    new Kunde { Id = 1, Name = "Kunde 1"},
                    new Kunde { Id = 2, Name = "Kunde 2"}
                });

            // Act
            List<Kunde> actual = _systemUnderTest.SucheKunden(filter: null);

            // Assert
            actual.ShouldNotBeNull();
            actual.Count.ShouldBe(expected: 2);

            _mockProtokollierer.Verify(
                expression: v => v.ProtokolliereInformation("2 Kunden gefunden"),
                times: Times.Once);
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
            actual.ShouldNotBeNull();
            actual.ShouldBeEmpty();

            _mockProtokollierer.Verify(
                expression: v => v.ProtokolliereFehler("Es ist ein Fehler aufgetreten"),
                times: Times.Once);
        }
    }
}
