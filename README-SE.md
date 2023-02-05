# Luxafor Device Controller

Ett .Net-bibliotek som tillhandahåller ett enkelt API för att styra Luxafor-enheter.

## Luxafor

#### Översikt över företaget

[Luxafor] (https://luxafor.com) är ett företag som utformar och säljer produkter för kontorsproduktivitet, t.ex. tillgänglighetsindikatorer och notifieringsverktyg. 

Deras flaggskeppsprodukt är en [LED availability indicator] (https://luxafor.com/product/flag) som kan programmeras för att visa olika färger beroende på användarens tillgänglighetsstatus. 

Luxafors mål är att ge användarna ett enkelt och effektivt sätt att signalera att de är tillgängliga för sina kollegor och förbättra kommunikationen och samarbetet på arbetsplatsen.

#### Snabb översikt över enheterna

Här är en icke uttömmande förteckning över [Luxafor-apparater] (https://luxafor.com/products):

- Luxafor Flag: en LED-indikator för tillgänglighet som visar personlig tillgänglighet.
- Luxafor Bluetooth": en trådlös, mjukvarustyrd LED-indikator för tillgänglighet som visar meddelanden och personlig tillgänglighet.
- Luxafor Switch: en trådlös, fjärrstyrd tillgänglighetsindikator som visar tillgängligheten för mötesrum och arbetsstationer i realtid.
- Luxafor Cube`: en fristående LED-indikator som visar om mötesrummen är tillgängliga.
- Luxafor Pomodoro-Timer`: en USB-driven LED-timer som gör det möjligt att dela upp arbetet i mindre delar (se [Pomodoro](https://reefact.net/craftsmanship/tools/pomodoro))).
- Luxafor Orb`: en LED-indikator för USB-tillgänglighet med bred vinkel.
- Luxafor CO2 Monitor: en sensor som analyserar luftkvaliteten i ett rum och varnar dig när det behöver ventileras.
- Luxafor Mute-knapp: Slå på/av mikrofonen med en enda beröring och indikera om du är tillgänglig med den röda/gröna
- Luxafor Colorblind Flag: monokrom USB LED-tillgänglighetslampa som eliminerar distraktioner och ökar produktiviteten.

#### Integration

Dessa olika enheter är utformade för att kunna drivas manuellt ("mekaniskt") för vissa, halvautomatiskt (manuell styrning via [programvara](https://luxaformanual.com)) eller automatiskt (integration via [programvara](https://luxaformanual.com) med verktyg som Teams, Skype, Cisco, Zappier eller via Webhook) för andra. 

## Presentation av biblioteket

Det här biblioteket syftar till att göra det möjligt att integrera USB LED-enheter i dina interna applikationer utan att behöva gå via Luxafor-servern (webhook).

Den är utvecklad i .Net Core och bygger på biblioteket [HidLibrairy] (https://github.com/mikeobrien/HidLibrary) som gör det möjligt att räkna upp och kommunicera med HID-kompatibla USB-enheter i .NET.

Koden nedan visar ett exempel på en grundläggande användning av biblioteket för att styra en [Luxafor Orb] (https://luxafor.com/product/orb/) enhet.

https://github.com/Reefact/luxafor-devices-controller/blob/eb984aebc8db58c9922f9b480706e946a8ef5d88/LuxaforDevicesController.UnitTests/UsageExamples.cs#L20-L32

Linje 21 visar hur du ansluter till en enda Orb som är ansluten till maskinens USB-port.

Jag ska snabbt gå igenom de möjliga kommandon som kan skickas till enheter från `LuxaforDevice`.

#### Stäng av

```csharp
void TurnOff(); // Stänger av alla lysdioder på enheten.
void TurnOff(TargetedLeds targetedLeds); // Stänger av den målinriktade enhetens lysdioder
```

### Ange en enda färg

```csharp
void SetColor(BasicColor basicColor); // Tänder alla enhetens lysdioder i en grundfärg.
void SetColor(CustomColor customColor customColor); // Tänder enhetens lysdioder i en egen färg.
void SetColor(TargetedLeds targetedLeds, BasicColor basicColor); // Tänder alla riktade lysdioder på enheten i en grundfärg.
void SetColor(TargetedLeds targetedLeds, CustomColor color); // Slår på de riktade enheternas lysdioder i en anpassad färg.
```

#### Gör en övergång (blekning)

```csharp
void FadeColor(BasicColor basicColor basicColor, FadeDuration duration); // Alla lysdioder på enheten övergår till en grundfärg.
void FadeColor(CustomColor color, FadeDuration duration); // Övergår alla lysdioder på enheten till en anpassad färg.
void FadeColor(TargetedLeds targetedLeds, BasicColor basicColor, FadeDuration duration); // Övergång till en grundfärg för enhetens målinriktade lysdioder.
void FadeColor(TargetedLeds targetedLeds, CustomColor color, FadeDuration duration); // Övergång av de riktade enhetens lysdioder till en anpassad färg.
```

#### Blink (stroboskopeffekt)

```csharp
void Strobe(BasicColor basicColor, Speed speed speed, Repeat repeat); // Blinkar alla enhetens lysdioder i en grundfärg.
void Strobe(CustomColor customColor customColor, Speed speed speed, Repeat repeat); // Blinkar alla lysdioder på enheten i en egen färg.
void Strobe(TargetedLeds targetedLeds, BasicColor basicColor, Speed speed speed, Repeat repeat); // Blinkar i en grundläggande färg på enhetens LED-dioder.
void Strobe(TargetedLeds targetedLeds, CustomColor customColor, Speed speed, Repeat repeat); // Blinkar för de LED-lampor som är målinriktade i en anpassad färg.
```

#### Vågor

```csharp
void Wave(WaveType waveType, BasicColor basicColor, Speed speed, Repeat repeat); // Startar ett "vågmönster" som riktar sig till alla lysdioder på enheten baserat på en grundfärg.
void Wave(WaveType waveType, CustomColor customColor, Speed speed, Repeat repeat); // Startar ett vågmönster som riktar sig till alla lysdioder på enheten baserat på en anpassad färg.
```

### Inbyggda mönster

```csharp
void PlayPattern(BuiltInPattern, Repeat repeat); // Starta ett inbyggt mönster som riktar sig till alla lysdioder på enheten.
```

### Skicka ett kommando

Det är möjligt att skapa egna kommandon som kallas `LightningCommand` så att de kan återanvändas i koden:

```csharp
var command = LightningCommand.CreateStrobeCommand(TargetedLeds.All, BasicColor.Yellow, Speed.FromByte(20), Repeat.Count(3));
```

Metoden `Send` gör det möjligt att använda dessa kommandon.

```csharp
void Send(LightningCommand command); // Skicka ett kommando till enheten
```