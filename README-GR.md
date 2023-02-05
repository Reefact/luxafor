# Ελεγκτής συσκευής Luxafor

Μια βιβλιοθήκη .Net που παρέχει ένα απλό API για τον έλεγχο των συσκευών Luxafor.

## Luxafor

### Επισκόπηση της εταιρείας

[Luxafor](https://luxafor.com) είναι μια εταιρεία που σχεδιάζει και πωλεί προϊόντα για την παραγωγικότητα του γραφείου, όπως δείκτες διαθεσιμότητας και εργαλεία ειδοποίησης. 

Η ναυαρχίδα τους είναι ένας [δείκτης διαθεσιμότητας LED] (https://luxafor.com/product/flag) που μπορεί να προγραμματιστεί ώστε να εμφανίζει διαφορετικά χρώματα ανάλογα με την κατάσταση διαθεσιμότητας του χρήστη. 

Στόχος της Luxafor είναι να παρέχει στους χρήστες έναν απλό και αποτελεσματικό τρόπο για να δηλώνουν τη διαθεσιμότητά τους στους συναδέλφους τους και να βελτιώνουν την επικοινωνία και τη συνεργασία στο χώρο εργασίας.

### Γρήγορη επισκόπηση των συσκευών

Ακολουθεί ένας μη εξαντλητικός κατάλογος [συσκευών Luxafor](https://luxafor.com/products):

- `Luxafor Flag`: μια ένδειξη διαθεσιμότητας LED που εμφανίζει την προσωπική διαθεσιμότητα
- `Luxafor Bluetooth`: μια ασύρματη, ελεγχόμενη από λογισμικό ένδειξη διαθεσιμότητας LED που εμφανίζει ειδοποιήσεις και προσωπική διαθεσιμότητα
- `Luxafor Switch`: ένας ασύρματος, τηλεχειριζόμενος δείκτης διαθεσιμότητας που εμφανίζει τη διαθεσιμότητα των αιθουσών συσκέψεων και των θέσεων εργασίας σε πραγματικό χρόνο.
- `Luxafor Cube`: μια αυτόνομη ένδειξη διαθεσιμότητας LED που εμφανίζει τη διαθεσιμότητα των αιθουσών συνεδριάσεων
- `Luxafor Pomodoro-Timer`: ένας χρονοδιακόπτης LED με τροφοδοσία USB που επιτρέπει τον διαχωρισμό της εργασίας σε μικρότερα χρονικά διαστήματα (βλ. [Pomodoro](https://reefact.net/craftsmanship/tools/pomodoro))
- `Luxafor Orb`: μια ευρυγώνια ένδειξη διαθεσιμότητας LED USB
- `Luxafor CO2 Monitor`: ένας αισθητήρας που αναλύει την ποιότητα του αέρα ενός δωματίου και σας προειδοποιεί όταν χρειάζεται εξαερισμός.
- `Κουμπί σίγασης Luxafor`: ενεργοποιήστε/απενεργοποιήστε το μικρόφωνο με ένα απλό άγγιγμα και δείξτε αν είστε διαθέσιμοι με το κόκκινο/πράσινο χρώμα.
- `Luxafor Colorblind Flag`: μονόχρωμο φως διαθεσιμότητας USB LED εξαλείφει τους περισπασμούς και ενισχύει την παραγωγικότητα

### Ενσωμάτωση

Αυτές οι διαφορετικές συσκευές έχουν σχεδιαστεί για να οδηγούνται χειροκίνητα ("μηχανικά") για ορισμένες, ημιαυτόματα (χειροκίνητη οδήγηση μέσω [λογισμικού](https://luxaformanual.com)) / αυτόματα (ενσωμάτωση μέσω [λογισμικού](https://luxaformanual.com) με εργαλεία όπως Teams, Skype, Cisco, Zappier ή μέσω Webhook) για άλλες. 

## Παρουσίαση της βιβλιοθήκης

Αυτή η βιβλιοθήκη έχει ως στόχο να επιτρέψει την ενσωμάτωση συσκευών LED USB στις εσωτερικές σας εφαρμογές χωρίς να χρειάζεται να περάσετε από τον διακομιστή Luxafor (webhook).

Έχει αναπτυχθεί σε .Net Core και βασίζεται στη βιβλιοθήκη [HidLibrairy] (https://github.com/mikeobrien/HidLibrary), η οποία επιτρέπει την απαρίθμηση και την επικοινωνία με συσκευές USB συμβατές με HID στο .NET.

Ο παρακάτω κώδικας δείχνει ένα παράδειγμα βασικής χρήσης της βιβλιοθήκης για την οδήγηση μιας συσκευής [Luxafor Orb](https://luxafor.com/product/orb/).

https://github.com/Reefact/luxafor-devices-controller/blob/eb984aebc8db58c9922f9b480706e946a8ef5d88/LuxaforDevicesController.UnitTests/UsageExamples.cs#L20-L32

Η γραμμή 21 δείχνει πώς να συνδεθείτε σε ένα μόνο Orb που είναι συνδεδεμένο στη θύρα USB του μηχανήματος.

Θα εξετάσω γρήγορα το σύνολο των πιθανών εντολών που μπορείτε να στείλετε σε συσκευές από το `LuxaforDevice`.

### Turn off

```csharp
void TurnOff(); // Απενεργοποιεί όλες τις λυχνίες LED της συσκευής
void TurnOff(TargetedLeds targetedLeds); // Απενεργοποίηση των LED της στοχευμένης συσκευής
```

### Ορίστε ένα μόνο χρώμα

```csharp
void SetColor(BasicColor basicColor); // Ενεργοποιεί όλες τις λυχνίες LED της συσκευής σε ένα βασικό χρώμα.
void SetColor(CustomColor customColor customColor); // Ενεργοποιεί τις λυχνίες LED της συσκευής σε ένα προσαρμοσμένο χρώμα.
void SetColor(TargetedLeds targetedLeds, BasicColor basicColor); // Ενεργοποιεί όλες τις στοχευμένες λυχνίες LED της συσκευής σε ένα βασικό χρώμα.
void SetColor(TargetedLeds targetedLeds, CustomColor color); // Ενεργοποιεί τα LED της στοχευμένης συσκευής σε ένα προσαρμοσμένο χρώμα.
```

### Κάντε μια μετάβαση (fade)

```csharp
void FadeColor(BasicColor basicColor, FadeDuration duration); // Μετάβαση όλων των LED της συσκευής σε ένα βασικό χρώμα
void FadeColor(CustomColor color, FadeDuration duration); // Μεταβαίνει όλα τα LED της συσκευής σε ένα προσαρμοσμένο χρώμα.
void FadeColor(TargetedLeds targetedLeds, BasicColor basicColor, FadeDuration duration) // Μετάβαση των στοχευμένων LED της συσκευής σε ένα βασικό χρώμα.
void FadeColor(TargetedLeds targetedLeds, CustomColor color, FadeDuration duration) // Μετάβαση των LED της στοχευμένης συσκευής σε ένα προσαρμοσμένο χρώμα
```

### Blink (στροβοσκόπιο)

```csharp
void Strobe(BasicColor basicColor, Speed speed, Repeat repeat); // Αναβοσβήνει όλα τα LED της συσκευής σε ένα βασικό χρώμα
void Strobe(CustomColor customColor, Speed speed, Repeat repeat); // Αναβοσβήνει όλα τα LED της συσκευής σε ένα προσαρμοσμένο χρώμα.
void Strobe(TargetedLeds targetedLeds, BasicColor basicColor, Speed speed, Repeat repeat); // Αναβοσβήνει τις στοχευμένες λυχνίες LED της συσκευής με ένα βασικό χρώμα.
void Strobe(TargetedLeds targetedLeds, CustomColor customColor, Speed speed, Repeat repeat); // Αναβοσβήνει τα LED της στοχευμένης συσκευής σε ένα προσαρμοσμένο χρώμα.
```

### Waves

```csharp
void Wave(WaveType waveType, BasicColor basicColor, Speed speed, Repeat repeat); // Ξεκινά ένα μοτίβο "κύματος" που στοχεύει όλα τα LED της συσκευής με βάση ένα βασικό χρώμα.
void Wave(WaveType waveType, CustomColor customColor, Speed speed, Repeat repeat); // Ξεκινάει ένα μοτίβο κύματος που στοχεύει όλα τα LED της συσκευής με βάση ένα προσαρμοσμένο χρώμα.
```

### Ενσωματωμένα μοτίβα

```csharp
void PlayPattern(BuiltInPattern, Repeat repeat); // Εκκίνηση ενός ενσωματωμένου μοτίβου που στοχεύει σε όλες τις λυχνίες LED της συσκευής
```

### Αποστολή εντολής

Είναι δυνατή η δημιουργία προσαρμοσμένων εντολών με την ονομασία `LightningCommand` ώστε να μπορούν να επαναχρησιμοποιηθούν στον κώδικα:

```csharp
var command = LightningCommand.CreateStrobeCommand(TargetedLeds.All, BasicColor.Yellow, Speed.FromByte(20), Repeat.Count(3)),
```

Η μέθοδος `Send` σας επιτρέπει να χρησιμοποιήσετε αυτές τις εντολές.

```csharp
void Send(LightningCommand command); // Αποστολή μιας εντολής στη συσκευή
```