using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using iPhone;

public class iPhoneScript : MonoBehaviour
{
    public KMBombInfo Bomb;
    public KMAudio Audio;

    //PIN
    public List<String> pinDigits;
    public List<String> pinOptions;
    string enteredPIN = "";
    string correctPIN;
    string solved = "";

    //Home screen buttons
    public KMSelectable angryBirds;
    public KMSelectable messages;
    public KMSelectable photos;
    public KMSelectable tinder;
    public KMSelectable phone;
    public KMSelectable settings;
    public KMSelectable home;
    string time;
    public TextMesh timeDisplay;
    public TextMesh networkDisplay;
    public Renderer bar1;
    public Renderer bar2;
    public Renderer bar3;
    public Renderer bar4;
    public Renderer battery;
    public Texture fullBatt;
    public Texture threeQuartBatt;
    public Texture quartBatt;
    public Texture lowBatt;
    float timeRemaining;

    //Angry Birds
    public KMSelectable TopLeft;
    public KMSelectable TopRight;
    public KMSelectable BottomLeft;
    public KMSelectable BottomRight;
    public Renderer stars;
    public List<Texture> angryBirdsOptions;
    public List<Renderer> angryBirdsButtons;
    public TextMesh digitAnswer1;
    private List<String> selectedABImages = new List<string>();
    string correctAngryBird;
    string angryBirdsLabel;
    string birdsLogged = "";
    string angryBirdsRule1;
    string angryBirdsRule2;
    string angryBirdsWin;

    //Messages
    public Renderer philDisc;
    public Renderer robDisc;
    public Renderer mickDisc;
    public Renderer andyDisc;
    public Renderer philMessage;
    public Renderer robMessage;
    public Renderer mickMessage;
    public Renderer andyMessage;
    public TextMesh philText;
    public TextMesh robText;
    public TextMesh mickText;
    public TextMesh andyText;
    private String truthTeller;
    string messagesLogged = "";
    public Renderer philUnread;
    public Renderer robUnread;
    public Renderer mickUnread;
    public Renderer andyUnread;
    string messageCheatAlert;

    //Photos
    public KMSelectable photoLeft;
    public KMSelectable photoRight;
    public Renderer photoScreen;
    public List<Texture> decoyPhotos;
    public List<Texture> truePhotos;
    private int selectedPhotosIndex;
    private List<Texture> selectedPhotoImages = new List<Texture>();
    int truePhotosSelection;
    public Renderer revealedPhoto;
    string photoRevealLight;
    string photosLogged = "";

    //Tinder
    public TextMesh tinderProfile;
    public KMSelectable swipeLeft;
    public KMSelectable swipeRight;
    private List<String> tinderNames = new List<string> { "Sophie", "Carol", "Charlie", "Jess", "Chloe", "Sarah", "Mary", "Megan", "Lisa", "Kate", "Lauren", "Freya", "Emma", "Frankie", "Barb", "Kate", "Juliet", "Shannon", "Sadie", "Ellie", "Emily" };
    private List<String> tinderAges = new List<string> { "18", "21", "24", "25", "28", "30", "32", "34", "37", "40", "41", "44", "48" };
    private List<String> tinderHobbies = new List<string> { "badminton", "golf", "the cinema", "the theatre", "dancing", "clubbing" };
    private List<String> tinderStarSign = new List<string> { "Virgo", "Leo", "Scorpio", "Capricorn", "Cancer", "Gemini" };
    private List<String> tinderPet = new List<string> { "cat", "dog", "goldfish", "gerbil", "hamster" };
    private String chosenTinderName;
    private String chosenTinderAge;
    private String chosenTinderHobby;
    private String chosenTinderStarSign;
    private String chosenTinderPet;
    private int tinderScore;
    int strikeCount;
    int stage = 1;
    int settingsStage = 1;
    bool tinderDone = false;

    //Phone
    public TextMesh phoneNumber;
    public KMSelectable oneButton;
    public KMSelectable twoButton;
    public KMSelectable threeButton;
    public KMSelectable fourButton;
    public KMSelectable fiveButton;
    public KMSelectable sixButton;
    public KMSelectable sevenButton;
    public KMSelectable eightButton;
    public KMSelectable nineButton;
    public KMSelectable starButton;
    public KMSelectable zeroButton;
    public KMSelectable hashButton;

    //Settings
    public TextMesh settingsText;
    public KMSelectable oneSButton;
    public KMSelectable twoSButton;
    public KMSelectable threeSButton;
    public KMSelectable fourSButton;
    public KMSelectable fiveSButton;
    public KMSelectable sixSButton;
    public KMSelectable sevenSButton;
    public KMSelectable eightSButton;
    public KMSelectable nineSButton;
    public KMSelectable zeroSButton;
    public Renderer pinScreen1;
    public Renderer pinScreen2;
    public Renderer pinScreen3;
    public Renderer pinScreen4;

    //Wallpapers
    public Renderer iPhoneScreen;
    public List<Texture> wallpaper;
    private int wallpaperIndex;
    public Texture angryBirdsBackground;
    public Texture blackBackground;
    public Texture whiteBackground;
    public Texture buttonMat;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;

    void Update()
    {
        strikeCount = Bomb.GetStrikes();
        timeRemaining = Bomb.GetTime();
        time = DateTime.Now.ToString("HH:mm");
        timeDisplay.text = time;

        if (timeRemaining >= 300)
        {
            battery.material.mainTexture = fullBatt;
        }
        else if (timeRemaining < 300 && timeRemaining >= 210)
        {
            battery.material.mainTexture = threeQuartBatt;
        }
        else if (timeRemaining < 210 && timeRemaining >= 90)
        {
            battery.material.mainTexture = quartBatt;
        }
        else
        {
            battery.material.mainTexture = lowBatt;
        }
    }

    void Awake()
    {
        moduleId = moduleIdCounter++;

        //Home screen buttons
        angryBirds.OnInteract += delegate () { HomeButtonPress(angryBirds); return false; };
        messages.OnInteract += delegate () { HomeButtonPress(messages); return false; };
        photos.OnInteract += delegate () { HomeButtonPress(photos); return false; };
        tinder.OnInteract += delegate () { HomeButtonPress(tinder); return false; };
        phone.OnInteract += delegate () { HomeButtonPress(phone); return false; };
        settings.OnInteract += delegate () { HomeButtonPress(settings); return false; };
        home.OnInteract += delegate () { OnhomeButton(); return false; };

        //Angry Birds buttons
        TopLeft.OnInteract += delegate () { BirdsPress(TopLeft); return false; };
        TopRight.OnInteract += delegate () { BirdsPress(TopRight); return false; };
        BottomLeft.OnInteract += delegate () { BirdsPress(BottomLeft); return false; };
        BottomRight.OnInteract += delegate () { BirdsPress(BottomRight); return false; };

        //Photos buttons
        photoLeft.OnInteract += delegate () { PhotoPress(photoLeft); return false; };
        photoRight.OnInteract += delegate () { PhotoPress(photoRight); return false; };

        //Tinder buttons
        swipeLeft.OnInteract += delegate () { TinderPress(swipeLeft); return false; };
        swipeRight.OnInteract += delegate () { TinderPress(swipeRight); return false; };

        //Phone buttons
        oneButton.OnInteract += delegate () { OnPhonePress("1"); return false; };
        twoButton.OnInteract += delegate () { OnPhonePress("2"); return false; };
        threeButton.OnInteract += delegate () { OnPhonePress("3"); return false; };
        fourButton.OnInteract += delegate () { OnPhonePress("4"); return false; };
        fiveButton.OnInteract += delegate () { OnPhonePress("5"); return false; };
        sixButton.OnInteract += delegate () { OnPhonePress("6"); return false; };
        sevenButton.OnInteract += delegate () { OnPhonePress("7"); return false; };
        eightButton.OnInteract += delegate () { OnPhonePress("8"); return false; };
        nineButton.OnInteract += delegate () { OnPhonePress("9"); return false; };
        starButton.OnInteract += delegate () { OnPhonePress("*"); return false; };
        zeroButton.OnInteract += delegate () { OnPhonePress("0"); return false; };
        hashButton.OnInteract += delegate () { OnHashButton(); return false; };

        //Settings buttons
        oneSButton.OnInteract += delegate () { OnSettingsPress("1"); return false; };
        twoSButton.OnInteract += delegate () { OnSettingsPress("2"); return false; };
        threeSButton.OnInteract += delegate () { OnSettingsPress("3"); return false; };
        fourSButton.OnInteract += delegate () { OnSettingsPress("4"); return false; };
        fiveSButton.OnInteract += delegate () { OnSettingsPress("5"); return false; };
        sixSButton.OnInteract += delegate () { OnSettingsPress("6"); return false; };
        sevenSButton.OnInteract += delegate () { OnSettingsPress("7"); return false; };
        eightSButton.OnInteract += delegate () { OnSettingsPress("8"); return false; };
        nineSButton.OnInteract += delegate () { OnSettingsPress("9"); return false; };
        zeroSButton.OnInteract += delegate () { OnSettingsPress("0"); return false; };
    }

    void Start()
    {
        wallpaperIndex = UnityEngine.Random.Range(0, 8);
        screenStarter();
        generatePIN();
        angryBirdsSetUp();
        angryBirdsGame();
        messageSetUp();
        photoSetUp();
        tinderSetUp();
        tinderLogic();
        phoneSetUp();
    }

    //Set the home screen
    void screenStarter()
    {
        TopLeft.gameObject.SetActive(false);
        TopRight.gameObject.SetActive(false);
        BottomLeft.gameObject.SetActive(false);
        BottomRight.gameObject.SetActive(false);
        stars.gameObject.SetActive(false);

        philDisc.gameObject.SetActive(false);
        robDisc.gameObject.SetActive(false);
        mickDisc.gameObject.SetActive(false);
        andyDisc.gameObject.SetActive(false);
        philMessage.gameObject.SetActive(false);
        robMessage.gameObject.SetActive(false);
        mickMessage.gameObject.SetActive(false);
        andyMessage.gameObject.SetActive(false);
        philUnread.gameObject.SetActive(false);
        robUnread.gameObject.SetActive(false);
        mickUnread.gameObject.SetActive(false);
        andyUnread.gameObject.SetActive(false);

        photoLeft.gameObject.SetActive(false);
        photoRight.gameObject.SetActive(false);
        photoScreen.gameObject.SetActive(false);
        revealedPhoto.gameObject.SetActive(false);

        tinderProfile.gameObject.SetActive(false);
        swipeLeft.gameObject.SetActive(false);
        swipeRight.gameObject.SetActive(false);

        oneButton.gameObject.SetActive(false);
        twoButton.gameObject.SetActive(false);
        threeButton.gameObject.SetActive(false);
        fourButton.gameObject.SetActive(false);
        fiveButton.gameObject.SetActive(false);
        sixButton.gameObject.SetActive(false);
        sevenButton.gameObject.SetActive(false);
        eightButton.gameObject.SetActive(false);
        nineButton.gameObject.SetActive(false);
        starButton.gameObject.SetActive(false);
        zeroButton.gameObject.SetActive(false);
        hashButton.gameObject.SetActive(false);
        phoneNumber.gameObject.SetActive(false);

        oneSButton.gameObject.SetActive(false);
        twoSButton.gameObject.SetActive(false);
        threeSButton.gameObject.SetActive(false);
        fourSButton.gameObject.SetActive(false);
        fiveSButton.gameObject.SetActive(false);
        sixSButton.gameObject.SetActive(false);
        sevenSButton.gameObject.SetActive(false);
        eightSButton.gameObject.SetActive(false);
        nineSButton.gameObject.SetActive(false);
        zeroSButton.gameObject.SetActive(false);
        settingsText.gameObject.SetActive(false);
        pinScreen1.gameObject.SetActive(false);
        pinScreen2.gameObject.SetActive(false);
        pinScreen3.gameObject.SetActive(false);
        pinScreen4.gameObject.SetActive(false);

        iPhoneScreen.material.mainTexture = wallpaper[wallpaperIndex];
        angryBirds.gameObject.SetActive(true);
        messages.gameObject.SetActive(true);
        photos.gameObject.SetActive(true);
        tinder.gameObject.SetActive(true);
        phone.gameObject.SetActive(true);
        settings.gameObject.SetActive(true);

        if (iPhoneScreen.material.mainTexture == wallpaper[2] || iPhoneScreen.material.mainTexture == wallpaper[3] || iPhoneScreen.material.mainTexture == wallpaper[5] || iPhoneScreen.material.mainTexture == wallpaper[7])
        {
            timeDisplay.color = Color.white;
            networkDisplay.color = Color.white;
            bar1.material.mainTexture = whiteBackground;
            bar2.material.mainTexture = whiteBackground;
            bar3.material.mainTexture = whiteBackground;
            bar4.material.mainTexture = whiteBackground;
        }
        else
        {
            timeDisplay.color = Color.black;
            networkDisplay.color = Color.black;
            bar1.material.mainTexture = blackBackground;
            bar2.material.mainTexture = blackBackground;
            bar3.material.mainTexture = blackBackground;
            bar4.material.mainTexture = blackBackground;
        }
    }

    //Set up apps and PIN
    void generatePIN()
    {
        for (int digit = 0; digit < pinDigits.Count; ++digit)
        {
            int digitIndex = 0;
            digitIndex = UnityEngine.Random.Range(0, 10);
            pinDigits[digit] = pinOptions[digitIndex];
        }
        Debug.LogFormat("[iPhone #{0}] The correct PIN is {1}{2}{3}{4}.", moduleId, pinDigits[0], pinDigits[1], pinDigits[2], pinDigits[3]);
    }

    void angryBirdsSetUp()
    {
        digitAnswer1.text = " ";
        foreach (Renderer texture in angryBirdsButtons)
        {
            int textureIndex = 0;
            textureIndex = UnityEngine.Random.Range(0, 10);
            texture.material.mainTexture = angryBirdsOptions[textureIndex];
            selectedABImages.Add(texture.material.mainTexture.name);
        }
    }

    void messageSetUp()
    {
        List<String> philTruth = new List<string> { "The 2nd number is " + pinDigits[1] + ".", "Not sure. Maybe " + pinDigits[1] + "?", pinDigits[1] + " mate." };
        List<String> robTruth = new List<string> { pinDigits[1] + "", pinDigits[1] + " is the second number.", "I think it's " + pinDigits[1] + "." };
        List<String> mickTruth = new List<string> { "It's " + pinDigits[1], pinDigits[1] + "? No...yes, " + pinDigits[1], pinDigits[1] + "?" };
        List<String> andyTruth = new List<string> { "Probably " + pinDigits[1], pinDigits[1] + " you numpty!", pinDigits[1] + "!" };

        int philWrongDigit = UnityEngine.Random.Range(0, 2);
        List<String> philLies = new List<string> { philWrongDigit + "", philWrongDigit + " is the second number.", "I think it's " + philWrongDigit + ".", "It's " + philWrongDigit, philWrongDigit + "? No...yes, " + philWrongDigit, philWrongDigit + "?", "Probably " + philWrongDigit, philWrongDigit + " you numpty!", philWrongDigit + "!" };
        int robWrongDigit = UnityEngine.Random.Range(2, 5);
        List<String> robLies = new List<string> { "The 2nd number is " + robWrongDigit + ".", "Not sure. Maybe " + robWrongDigit + "?", robWrongDigit + " mate.", "It's " + robWrongDigit, robWrongDigit + "? No...yes, " + robWrongDigit, robWrongDigit + "?", "Probably " + robWrongDigit, robWrongDigit + " you numpty!", robWrongDigit + "!" };
        int mickWrongDigit = UnityEngine.Random.Range(5, 8);
        List<String> mickLies = new List<string> { "The 2nd number is " + mickWrongDigit + ".", "Not sure. Maybe " + mickWrongDigit + "?", mickWrongDigit + " mate.", mickWrongDigit + "", mickWrongDigit + " is the second number.", "I think it's " + mickWrongDigit + ".", "Probably " + mickWrongDigit, mickWrongDigit + " you numpty!", mickWrongDigit + "!" };
        int andyWrongDigit = UnityEngine.Random.Range(0, 10);
        List<String> andyLies = new List<string> { "The 2nd number is " + andyWrongDigit + ".", "Not sure. Maybe " + andyWrongDigit + "?", andyWrongDigit + " mate.", andyWrongDigit + "", andyWrongDigit + " is the second number.", "I think it's " + andyWrongDigit + ".", "It's " + andyWrongDigit, andyWrongDigit + "? No...yes, " + andyWrongDigit, andyWrongDigit + "?" }; ;


        int truthIndex = UnityEngine.Random.Range(0, 4);
        if (truthIndex == 0)
        {
            truthTeller = "Phil";
        }
        else if (truthIndex == 1)
        {
            truthTeller = "Rob";
        }
        else if (truthIndex == 2)
        {
            truthTeller = "Mick";
        }
        else if (truthIndex == 3)
        {
            truthTeller = "Andy";
        }

        int messagePicker = UnityEngine.Random.Range(0, 3);
        int philLiePicker = UnityEngine.Random.Range(0, 9);
        int robLiePicker = UnityEngine.Random.Range(0, 9);
        int mickLiePicker = UnityEngine.Random.Range(0, 9);
        int andyLiePicker = UnityEngine.Random.Range(0, 9);
        if (truthTeller == "Phil")
        {
            philText.text = philTruth[messagePicker];
            robText.text = robLies[robLiePicker];
            mickText.text = mickLies[mickLiePicker];
            andyText.text = andyLies[andyLiePicker];
        }
        else if (truthTeller == "Rob")
        {
            philText.text = philLies[philLiePicker];
            robText.text = robTruth[messagePicker];
            mickText.text = mickLies[mickLiePicker];
            andyText.text = andyLies[andyLiePicker];
        }
        else if (truthTeller == "Mick")
        {
            philText.text = philLies[philLiePicker];
            robText.text = robLies[robLiePicker];
            mickText.text = mickTruth[messagePicker];
            andyText.text = andyLies[andyLiePicker];
        }
        else if (truthTeller == "Andy")
        {
            philText.text = philLies[philLiePicker];
            robText.text = robLies[robLiePicker];
            mickText.text = mickLies[mickLiePicker];
            andyText.text = andyTruth[messagePicker];
        }

    }
    void photoSetUp()
    {
        while (selectedPhotoImages.Count < 7)
        {
            getPhotos();
        }

        if (int.TryParse(pinDigits[2], out truePhotosSelection))
        {
            selectedPhotoImages.Add(truePhotos[truePhotosSelection]);
        }
        else
        {
            selectedPhotoImages.Add(truePhotos[truePhotosSelection]);
        }
    }

    void phoneSetUp()
    {
        phoneNumber.text = "";
    }

    void tinderSetUp()
    {
        tinderScore = 0;

        int tinderNameIndex = UnityEngine.Random.Range(0, tinderNames.Count);
        chosenTinderName = tinderNames[tinderNameIndex];

        int tinderAgeIndex = UnityEngine.Random.Range(0, tinderAges.Count);
        chosenTinderAge = tinderAges[tinderAgeIndex];

        int tinderHobbyIndex = UnityEngine.Random.Range(0, tinderHobbies.Count);
        chosenTinderHobby = tinderHobbies[tinderHobbyIndex];

        int tinderStarSignIndex = UnityEngine.Random.Range(0, tinderStarSign.Count);
        chosenTinderStarSign = tinderStarSign[tinderStarSignIndex];

        int tinderPetIndex = UnityEngine.Random.Range(0, tinderPet.Count);
        chosenTinderPet = tinderPet[tinderPetIndex];

        tinderProfile.text = chosenTinderName + ", " + chosenTinderAge + "\n" + chosenTinderStarSign + "\n\nEnjoys " + chosenTinderHobby + "\nHas a pet " + chosenTinderPet;
    }

    //App logic methods
    void angryBirdsGame()
    {
        if (selectedABImages.Where((x) => x.Contains("Bird")).Count() >= 3) //More birds present
        {
            angryBirdsRule1 = "A";
            if (Bomb.GetBatteryCount() >= 3)
            {
                angryBirdsRule2 = "D";
                if ((selectedABImages[0] == "Yellow Angry Bird" && selectedABImages[2].Contains("Pig")) || (selectedABImages[1] == "Yellow Angry Bird" && selectedABImages[3].Contains("Pig")))
                {
                    correctAngryBird = "Top Right";
                }
                else if ((selectedABImages[1] == "Black Angry Bird" && selectedABImages[0] == "Red Angry Bird") || (selectedABImages[3] == "Black Angry Bird" && selectedABImages[2] == "Red Angry Bird"))
                {
                    correctAngryBird = "Top Left";
                }
                else if (selectedABImages.Where((x) => x.Contains("White")).Count() >= 1)
                {
                    correctAngryBird = "Bottom Left";
                }
                else
                {
                    correctAngryBird = "Bottom Right";
                }
            }
            else if (Bomb.GetIndicators().Count() >= 3)
            {
                angryBirdsRule2 = "E";
                if ((selectedABImages[0] == "Yellow Angry Bird" && selectedABImages[2].Contains("Pig")) || (selectedABImages[1] == "Yellow Angry Bird" && selectedABImages[3].Contains("Pig")))
                {
                    correctAngryBird = "Bottom Left";
                }
                else if ((selectedABImages[1] == "Black Angry Bird" && selectedABImages[0] == "Red Angry Bird") || (selectedABImages[3] == "Black Angry Bird" && selectedABImages[2] == "Red Angry Bird"))
                {
                    correctAngryBird = "Top Right";
                }
                else if (selectedABImages.Where((x) => x.Contains("White")).Count() >= 1)
                {
                    correctAngryBird = "Bottom Right";
                }
                else
                {
                    correctAngryBird = "Top Left";
                }
            }
            else
            {
                angryBirdsRule2 = "F";
                if ((selectedABImages[0] == "Yellow Angry Bird" && selectedABImages[2].Contains("Pig")) || (selectedABImages[1] == "Yellow Angry Bird" && selectedABImages[3].Contains("Pig")))
                {
                    correctAngryBird = "Top Left";
                }
                else if ((selectedABImages[1] == "Black Angry Bird" && selectedABImages[0] == "Red Angry Bird") || (selectedABImages[3] == "Black Angry Bird" && selectedABImages[2] == "Red Angry Bird"))
                {
                    correctAngryBird = "Bottom Right";
                }
                else if (selectedABImages.Where((x) => x.Contains("White")).Count() >= 1)
                {
                    correctAngryBird = "Top Right";
                }
                else
                {
                    correctAngryBird = "Bottom Left";
                }
            }
        }
        else if (selectedABImages.Where((x) => x.Contains("Pig")).Count() >= 3) //More pigs present
        {
            angryBirdsRule1 = "B";
            if (Bomb.GetBatteryCount() >= 3)
            {
                angryBirdsRule2 = "G";
                if (selectedABImages.Distinct().Count() == 4)
                {
                    correctAngryBird = "Bottom Left";
                }
                else if (!selectedABImages.Any((x) => x.Equals("King Pig")))
                {
                    correctAngryBird = "Top Right";
                }
                else if ((selectedABImages[0] == "Helmet Pig" && selectedABImages[1].Contains("Bird")) || (selectedABImages[0] == "Moustached Pig" && selectedABImages[1].Contains("Bird")) || (selectedABImages[2] == "Helmet Pig" && selectedABImages[3].Contains("Bird")) || (selectedABImages[2] == "Moustached Pig" && selectedABImages[3].Contains("Bird")))
                {
                    correctAngryBird = "Top Left";
                }
                else
                {
                    correctAngryBird = "Bottom Right";
                }
            }
            else if (Bomb.GetIndicators().Count() >= 3)
            {
                angryBirdsRule2 = "H";
                if (selectedABImages.Distinct().Count() == 4)
                {
                    correctAngryBird = "Bottom Right";
                }
                else if (!selectedABImages.Any((x) => x.Equals("King Pig")))
                {
                    correctAngryBird = "Bottom Left";
                }
                else if ((selectedABImages[0] == "Helmet Pig" && selectedABImages[1].Contains("Bird")) || (selectedABImages[0] == "Moustached Pig" && selectedABImages[1].Contains("Bird")) || (selectedABImages[2] == "Helmet Pig" && selectedABImages[3].Contains("Bird")) || (selectedABImages[2] == "Moustached Pig" && selectedABImages[3].Contains("Bird")))
                {
                    correctAngryBird = "Top Right";
                }
                else
                {
                    correctAngryBird = "Top Left";
                }
            }
            else
            {
                angryBirdsRule2 = "I";
                if (selectedABImages.Distinct().Count() == 4)
                {
                    correctAngryBird = "Top Right";
                }
                else if (!selectedABImages.Any((x) => x.Equals("King Pig")))
                {
                    correctAngryBird = "Top Left";
                }
                else if ((selectedABImages[0] == "Helmet Pig" && selectedABImages[1].Contains("Bird")) || (selectedABImages[0] == "Moustached Pig" && selectedABImages[1].Contains("Bird")) || (selectedABImages[2] == "Helmet Pig" && selectedABImages[3].Contains("Bird")) || (selectedABImages[2] == "Moustached Pig" && selectedABImages[3].Contains("Bird")))
                {
                    correctAngryBird = "Bottom Right";
                }
                else
                {
                    correctAngryBird = "Bottom Left";
                }
            }
        }
        else
        {
            angryBirdsRule1 = "C";
            if (Bomb.GetBatteryCount() >= 3)
            {
                angryBirdsRule2 = "J";
                if (selectedABImages.Take(2).Any((x) => x.Equals("Regular Pig")) && (selectedABImages.Take(2).Any((x) => x.Equals("White Angry Bird")) || selectedABImages.Take(2).Any((x) => x.Equals("Blue Angry Bird"))))
                {
                    correctAngryBird = "Top Left";
                }
                else if ((selectedABImages[0].Contains("Pig") && (selectedABImages[2] == "Red Angry Bird" || selectedABImages[2] == "Black Angry Bird")) || (selectedABImages[1].Contains("Pig") && (selectedABImages[3] == "Red Angry Bird" || selectedABImages[3] == "Black Angry Bird")))
                {
                    correctAngryBird = "Bottom Left";
                }
                else if (selectedABImages.Distinct().Count() == 4)
                {
                    correctAngryBird = "Bottom Right";
                }
                else
                {
                    correctAngryBird = "Top Right";
                }
            }
            else if (Bomb.GetIndicators().Count() >= 3)
            {
                angryBirdsRule2 = "K";
                if (selectedABImages.Take(2).Any((x) => x.Equals("Regular Pig")) && (selectedABImages.Take(2).Any((x) => x.Equals("White Angry Bird")) || selectedABImages.Take(2).Any((x) => x.Equals("Blue Angry Bird"))))
                {
                    correctAngryBird = "Bottom Left";
                }
                else if ((selectedABImages[0].Contains("Pig") && (selectedABImages[2] == "Red Angry Bird" || selectedABImages[2] == "Black Angry Bird")) || (selectedABImages[1].Contains("Pig") && (selectedABImages[3] == "Red Angry Bird" || selectedABImages[3] == "Black Angry Bird")))
                {
                    correctAngryBird = "Top Right";
                }
                else if (selectedABImages.Distinct().Count() == 4)
                {
                    correctAngryBird = "Top Left";
                }
                else
                {
                    correctAngryBird = "Bottom Right";
                }
            }
            else
            {
                angryBirdsRule2 = "L";
                if (selectedABImages.Take(2).Any((x) => x.Equals("Regular Pig")) && (selectedABImages.Take(2).Any((x) => x.Equals("White Angry Bird")) || selectedABImages.Take(2).Any((x) => x.Equals("Blue Angry Bird"))))
                {
                    correctAngryBird = "Top Right";
                }
                else if ((selectedABImages[0].Contains("Pig") && (selectedABImages[2] == "Red Angry Bird" || selectedABImages[2] == "Black Angry Bird")) || (selectedABImages[1].Contains("Pig") && (selectedABImages[3] == "Red Angry Bird" || selectedABImages[3] == "Black Angry Bird")))
                {
                    correctAngryBird = "Bottom Right";
                }
                else if (selectedABImages.Distinct().Count() == 4)
                {
                    correctAngryBird = "Bottom Left";
                }
                else
                {
                    correctAngryBird = "Top Left";
                }
            }
        }
        if (correctAngryBird == "Top Left")
        {
            angryBirdsLabel = angryBirdsButtons[0].material.mainTexture.name;
        }
        else if (correctAngryBird == "Top Right")
        {
            angryBirdsLabel = angryBirdsButtons[1].material.mainTexture.name;
        }
        else if (correctAngryBird == "Bottom Left")
        {
            angryBirdsLabel = angryBirdsButtons[2].material.mainTexture.name;
        }
        else if (correctAngryBird == "Bottom Right")
        {
            angryBirdsLabel = angryBirdsButtons[3].material.mainTexture.name;
        }
    }

    void getPhotos()
    {
        int photoOptions = UnityEngine.Random.Range(0, 29);
        if (!selectedPhotoImages.Contains(decoyPhotos[photoOptions]))
        {
            selectedPhotoImages.Add(decoyPhotos[photoOptions]);
        }
    }

    void tinderLogic()
    {
        if (strikeCount < 1)
        {
            if (chosenTinderAge == "18" || chosenTinderAge == "21")
            {
                tinderScore += 3;
            }
            else if (chosenTinderAge == "24" || chosenTinderAge == "25" || chosenTinderAge == "28")
            {
                tinderScore += 2;
            }
            else if (chosenTinderAge == "30" || chosenTinderAge == "32" || chosenTinderAge == "34")
            {
                tinderScore += 1;
            }
            else if (chosenTinderAge == "37" || chosenTinderAge == "40" || chosenTinderAge == "41")
            {
                tinderScore -= 1;
            }
            else if (chosenTinderAge == "44" || chosenTinderAge == "48")
            {
                tinderScore -= 2;
            }

            if (chosenTinderHobby == "badminton")
            {
                tinderScore += 1;
            }
            else if (chosenTinderHobby == "golf")
            {
                tinderScore -= 1;
            }
            else if (chosenTinderHobby == "the cinema")
            {
                tinderScore += 1;
            }
            else if (chosenTinderHobby == "the theatre")
            {
                tinderScore -= 2;
            }
            else if (chosenTinderHobby == "dancing")
            {
                tinderScore -= 3;
            }
            else if (chosenTinderHobby == "clubbing")
            {
                tinderScore += 2;
            }

            if (chosenTinderStarSign == "Virgo")
            {
                tinderScore += 2;
            }
            else if (chosenTinderStarSign == "Leo")
            {
                tinderScore += 2;
            }
            else if (chosenTinderStarSign == "Scorpio")
            {
                tinderScore -= 2;
            }
            else if (chosenTinderStarSign == "Capricorn")
            {
                tinderScore -= 2;
            }
            else if (chosenTinderStarSign == "Cancer")
            {
                tinderScore += 1;
            }
            else if (chosenTinderStarSign == "Gemini")
            {
                tinderScore -= 1;
            }

            if (chosenTinderPet == "cat")
            {
                tinderScore += 3;
            }
            else if (chosenTinderPet == "dog")
            {
                tinderScore += 2;
            }
            else if (chosenTinderPet == "goldfish")
            {
                tinderScore -= 1;
            }
            else if (chosenTinderPet == "gerbil")
            {
                tinderScore -= 2;
            }
            else if (chosenTinderPet == "hamster")
            {
                tinderScore -= 2;
            }
        }
        else if (strikeCount == 1)
        {
            if (chosenTinderAge == "18" || chosenTinderAge == "21")
            {
                tinderScore -= 2;
            }
            else if (chosenTinderAge == "24" || chosenTinderAge == "25" || chosenTinderAge == "28")
            {
                tinderScore -= 1;
            }
            else if (chosenTinderAge == "30" || chosenTinderAge == "32" || chosenTinderAge == "34")
            {
                tinderScore += 3;
            }
            else if (chosenTinderAge == "37" || chosenTinderAge == "40" || chosenTinderAge == "41")
            {
                tinderScore += 2;
            }
            else if (chosenTinderAge == "44" || chosenTinderAge == "48")
            {
                tinderScore -= 1;
            }

            if (chosenTinderHobby == "badminton")
            {
                tinderScore += 2;
            }
            else if (chosenTinderHobby == "golf")
            {
                tinderScore += 1;
            }
            else if (chosenTinderHobby == "the cinema")
            {
                tinderScore -= 1;
            }
            else if (chosenTinderHobby == "the theatre")
            {
                tinderScore += 1;
            }
            else if (chosenTinderHobby == "dancing")
            {
                tinderScore -= 2;
            }
            else if (chosenTinderHobby == "clubbing")
            {
                tinderScore -= 2;
            }

            if (chosenTinderStarSign == "Virgo")
            {
                tinderScore += 1;
            }
            else if (chosenTinderStarSign == "Leo")
            {
                tinderScore += 2;
            }
            else if (chosenTinderStarSign == "Scorpio")
            {
                tinderScore -= 1;
            }
            else if (chosenTinderStarSign == "Capricorn")
            {
                tinderScore -= 2;
            }
            else if (chosenTinderStarSign == "Cancer")
            {
                tinderScore += 2;
            }
            else if (chosenTinderStarSign == "Gemini")
            {
                tinderScore -= 1;
            }

            if (chosenTinderPet == "cat")
            {
                tinderScore += 1;
            }
            else if (chosenTinderPet == "dog")
            {
                tinderScore -= 3;
            }
            else if (chosenTinderPet == "goldfish")
            {
                tinderScore += 1;
            }
            else if (chosenTinderPet == "gerbil")
            {
                tinderScore += 2;
            }
            else if (chosenTinderPet == "hamster")
            {
                tinderScore -= 2;
            }
        }
        else if (strikeCount > 1)
        {
            if (chosenTinderAge == "18" || chosenTinderAge == "21")
            {
                tinderScore -= 1;
            }
            else if (chosenTinderAge == "24" || chosenTinderAge == "25" || chosenTinderAge == "28")
            {
                tinderScore -= 1;
            }
            else if (chosenTinderAge == "30" || chosenTinderAge == "32" || chosenTinderAge == "34")
            {
                tinderScore -= 2;
            }
            else if (chosenTinderAge == "37" || chosenTinderAge == "40" || chosenTinderAge == "41")
            {
                tinderScore += 3;
            }
            else if (chosenTinderAge == "44" || chosenTinderAge == "48")
            {
                tinderScore += 2;
            }

            if (chosenTinderHobby == "badminton")
            {
                tinderScore -= 1;
            }
            else if (chosenTinderHobby == "golf")
            {
                tinderScore += 1;
            }
            else if (chosenTinderHobby == "the cinema")
            {
                tinderScore -= 2;
            }
            else if (chosenTinderHobby == "the theatre")
            {
                tinderScore += 2;
            }
            else if (chosenTinderHobby == "dancing")
            {
                tinderScore += 3;
            }
            else if (chosenTinderHobby == "clubbing")
            {
                tinderScore -= 3;
            }

            if (chosenTinderStarSign == "Virgo")
            {
                tinderScore += 1;
            }
            else if (chosenTinderStarSign == "Leo")
            {
                tinderScore -= 1;
            }
            else if (chosenTinderStarSign == "Scorpio")
            {
                tinderScore += 1;
            }
            else if (chosenTinderStarSign == "Capricorn")
            {
                tinderScore -= 1;
            }
            else if (chosenTinderStarSign == "Cancer")
            {
                tinderScore += 1;
            }
            else if (chosenTinderStarSign == "Gemini")
            {
                tinderScore -= 1;
            }

            if (chosenTinderPet == "cat")
            {
                tinderScore -= 1;
            }
            else if (chosenTinderPet == "dog")
            {
                tinderScore -= 2;
            }
            else if (chosenTinderPet == "goldfish")
            {
                tinderScore += 2;
            }
            else if (chosenTinderPet == "gerbil")
            {
                tinderScore -= 2;
            }
            else if (chosenTinderPet == "hamster")
            {
                tinderScore += 3;
            }
        }
    }

    //Buttons
    public void OnhomeButton()
    {
        if (solved == "solved")
        {
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            GetComponent<KMSelectable>().AddInteractionPunch();
        }
        else
        {
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            GetComponent<KMSelectable>().AddInteractionPunch();
            screenStarter();
        }
    }

    void blackText()
    {
        timeDisplay.color = Color.black;
        networkDisplay.color = Color.black;
        bar1.material.mainTexture = blackBackground;
        bar2.material.mainTexture = blackBackground;
        bar3.material.mainTexture = blackBackground;
        bar4.material.mainTexture = blackBackground;
    }

    void whiteText()
    {
        timeDisplay.color = Color.white;
        networkDisplay.color = Color.white;
        bar1.material.mainTexture = whiteBackground;
        bar2.material.mainTexture = whiteBackground;
        bar3.material.mainTexture = whiteBackground;
        bar4.material.mainTexture = whiteBackground;
    }
    public void HomeButtonPress(KMSelectable homeButtonName)
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch();
        angryBirds.gameObject.SetActive(false);
        messages.gameObject.SetActive(false);
        photos.gameObject.SetActive(false);
        tinder.gameObject.SetActive(false);
        phone.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);

        if (homeButtonName == angryBirds)
        {
            if (birdsLogged == "")
            {
                Debug.LogFormat("[iPhone #{0}] Angry Birds: The chosen Angry Birds images are {1}.", moduleId, string.Join(", ", angryBirdsButtons.Select((x) => x.material.mainTexture.name).ToArray()));
                Debug.LogFormat("[iPhone #{0}] Angry Birds: The correct rule sets are {1} & {2}.", moduleId, angryBirdsRule1, angryBirdsRule2);
                Debug.LogFormat("[iPhone #{0}] Angry Birds: The correct image is {1} ({2}).", moduleId, angryBirdsLabel, correctAngryBird);
                birdsLogged = "logged";
            }
            if (angryBirdsWin != "true")
            {
                Audio.PlaySoundAtTransform("birdsStart", transform);
            }
            blackText();
            iPhoneScreen.material.mainTexture = angryBirdsBackground;
            TopLeft.gameObject.SetActive(true);
            TopRight.gameObject.SetActive(true);
            BottomLeft.gameObject.SetActive(true);
            BottomRight.gameObject.SetActive(true);
            if (angryBirdsWin == "true")
            {
                stars.gameObject.SetActive(true);
            }
        }
        else if (homeButtonName == messages)
        {
            if (messagesLogged == "")
            {
                Debug.LogFormat("[iPhone #{0}] Messages: Phil said '{1}'.", moduleId, philText.text);
                Debug.LogFormat("[iPhone #{0}] Messages: Rob said '{1}'.", moduleId, robText.text);
                Debug.LogFormat("[iPhone #{0}] Messages: Mick said '{1}'.", moduleId, mickText.text);
                Debug.LogFormat("[iPhone #{0}] Messages: Andy said '{1}'.", moduleId, andyText.text);
                Debug.LogFormat("[iPhone #{0}] Messages: The truth teller is '{1}'.", moduleId, truthTeller);
                messagesLogged = "logged";
            }
            if (messageCheatAlert == "true")
            {
                messageCheat();
            }
            blackText();
            iPhoneScreen.material.mainTexture = whiteBackground;
            philDisc.gameObject.SetActive(true);
            robDisc.gameObject.SetActive(true);
            mickDisc.gameObject.SetActive(true);
            andyDisc.gameObject.SetActive(true);
            philMessage.gameObject.SetActive(true);
            robMessage.gameObject.SetActive(true);
            mickMessage.gameObject.SetActive(true);
            andyMessage.gameObject.SetActive(true);
        }
        else if (homeButtonName == photos)
        {
            if (photosLogged == "")
            {
                Debug.LogFormat("[iPhone #{0}] Photos: The chosen photos are {1}.", moduleId, string.Join(", ", selectedPhotoImages.Select((x) => x.name).ToArray()));
                Debug.LogFormat("[iPhone #{0}] Photos: The correct image is {1}.", moduleId, truePhotos[truePhotosSelection].name);
                photosLogged = "logged";
            }
            whiteText();
            iPhoneScreen.material.mainTexture = blackBackground;
            photoLeft.gameObject.SetActive(true);
            photoRight.gameObject.SetActive(true);
            photoScreen.gameObject.SetActive(true);
            selectedPhotosIndex = UnityEngine.Random.Range(0, selectedPhotoImages.Count);
            photoScreen.material.mainTexture = selectedPhotoImages[selectedPhotosIndex];
        }
        else if (homeButtonName == tinder)
        {
            if (tinderDone == false)
            {
                tinderSetUp();
                tinderLogic();
                tinderLog();
            }
            blackText();
            iPhoneScreen.material.mainTexture = whiteBackground;
            tinderProfile.gameObject.SetActive(true);
            swipeLeft.gameObject.SetActive(true);
            swipeRight.gameObject.SetActive(true);
        }
        else if (homeButtonName == phone)
        {
            blackText();
            iPhoneScreen.material.mainTexture = whiteBackground;
            phoneNumber.gameObject.SetActive(true);
            oneButton.gameObject.SetActive(true);
            twoButton.gameObject.SetActive(true);
            threeButton.gameObject.SetActive(true);
            fourButton.gameObject.SetActive(true);
            fiveButton.gameObject.SetActive(true);
            sixButton.gameObject.SetActive(true);
            sevenButton.gameObject.SetActive(true);
            eightButton.gameObject.SetActive(true);
            nineButton.gameObject.SetActive(true);
            starButton.gameObject.SetActive(true);
            zeroButton.gameObject.SetActive(true);
            hashButton.gameObject.SetActive(true);
        }
        else if (homeButtonName == settings)
        {
            whiteText();
            iPhoneScreen.material.mainTexture = blackBackground;
            oneSButton.gameObject.SetActive(true);
            twoSButton.gameObject.SetActive(true);
            threeSButton.gameObject.SetActive(true);
            fourSButton.gameObject.SetActive(true);
            fiveSButton.gameObject.SetActive(true);
            sixSButton.gameObject.SetActive(true);
            sevenSButton.gameObject.SetActive(true);
            eightSButton.gameObject.SetActive(true);
            nineSButton.gameObject.SetActive(true);
            zeroSButton.gameObject.SetActive(true);
            settingsText.gameObject.SetActive(true);
            pinScreen1.gameObject.SetActive(true);
            pinScreen2.gameObject.SetActive(true);
            pinScreen3.gameObject.SetActive(true);
            pinScreen4.gameObject.SetActive(true);
        }
    }

    void tinderLog()
    {
        Debug.LogFormat("[iPhone #{0}] Tinder: Your match's name is {1}. She is {2} years old and her star sign is {3}. She enjoys {4} and has a pet {5}. You have {6} strike(s).", moduleId, chosenTinderName, chosenTinderAge, chosenTinderStarSign, chosenTinderHobby, chosenTinderPet, strikeCount);
        if (tinderScore == 0)
        {
            Debug.LogFormat("[iPhone #{0}] Tinder: Your match score is 0. Your match has {1} letters in her name. Swipe left if less than 5, swipe right if 5 or more.", moduleId, chosenTinderName.Length);
        }
        else
        {
            Debug.LogFormat("[iPhone #{0}] Tinder: Your match score is {1}. Swipe left if negative, swipe right if postive.", moduleId, tinderScore);
        }
    }
    public void BirdsPress(KMSelectable birdsButtonName)
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch();

        if (birdsButtonName == TopLeft && correctAngryBird == "Top Left")
        {
            digitAnswer1.text = pinDigits[0];
            stars.gameObject.SetActive(true);
            angryBirdsWin = "true";
            Audio.PlaySoundAtTransform("birdsWin", transform);
            Debug.LogFormat("[iPhone #{0}] Angry Birds: You pressed {1} and revealed the first PIN digit.", moduleId, correctAngryBird);
        }
        else if (birdsButtonName == TopRight && correctAngryBird == "Top Right")
        {
            digitAnswer1.text = pinDigits[0];
            stars.gameObject.SetActive(true);
            angryBirdsWin = "true";
            Audio.PlaySoundAtTransform("birdsWin", transform);
            Debug.LogFormat("[iPhone #{0}] Angry Birds: You pressed {1} and revealed the first PIN digit.", moduleId, correctAngryBird);
        }
        else if (birdsButtonName == BottomLeft && correctAngryBird == "Bottom Left")
        {
            digitAnswer1.text = pinDigits[0];
            stars.gameObject.SetActive(true);
            angryBirdsWin = "true";
            Audio.PlaySoundAtTransform("birdsWin", transform);
            Debug.LogFormat("[iPhone #{0}] Angry Birds: You pressed {1} and revealed the first PIN digit.", moduleId, correctAngryBird);
        }
        else if (birdsButtonName == BottomRight && correctAngryBird == "Bottom Right")
        {
            digitAnswer1.text = pinDigits[0];
            stars.gameObject.SetActive(true);
            angryBirdsWin = "true";
            Audio.PlaySoundAtTransform("birdsWin", transform);
            Debug.LogFormat("[iPhone #{0}] Angry Birds: You pressed {1} and revealed the first PIN digit.", moduleId, correctAngryBird);
        }
        else
        {
            Audio.PlaySoundAtTransform("birdsFail", transform);
            GetComponent<KMBombModule>().HandleStrike();
            strikeCount = Bomb.GetStrikes();
            Debug.LogFormat("[iPhone #{0}] Angry Birds: Strike! You pressed {1}. I was expecting {2}. You have {3} strike(s).", moduleId, birdsButtonName.name, correctAngryBird, strikeCount);
            if (tinderDone == false)
            {
                tinderSetUp();
                tinderLogic();
            }
        }
    }

    public void PhotoPress(KMSelectable photoButton)
    {
        if (photoButton == photoLeft)
        {
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            photoLeft.AddInteractionPunch(.5f);
            selectedPhotosIndex = ((selectedPhotosIndex + selectedPhotoImages.Count) - 1) % selectedPhotoImages.Count;
            photoScreen.material.mainTexture = selectedPhotoImages[selectedPhotosIndex];

            if (photoRevealLight == "true" && photoScreen.material.mainTexture == truePhotos[truePhotosSelection])
            {
                revealedPhoto.gameObject.SetActive(true);
            }
            else if (photoScreen.material.mainTexture != truePhotos[truePhotosSelection])
            {
                revealedPhoto.gameObject.SetActive(false);
            }
        }

        else if (photoButton == photoRight)
        {
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            photoRight.AddInteractionPunch(.5f);
            selectedPhotosIndex = (selectedPhotosIndex + 1) % selectedPhotoImages.Count;
            photoScreen.material.mainTexture = selectedPhotoImages[selectedPhotosIndex];

            if (photoRevealLight == "true" && photoScreen.material.mainTexture == truePhotos[truePhotosSelection])
            {
                revealedPhoto.gameObject.SetActive(true);
            }
            else if (photoScreen.material.mainTexture != truePhotos[truePhotosSelection])
            {
                revealedPhoto.gameObject.SetActive(false);
            }
        }
    }

    public void TinderPress(KMSelectable tinderButton)
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch();

        switch (stage)
        {
            case 1:
                if (tinderButton == swipeLeft && tinderScore < 0)
                {
                    Debug.LogFormat("[iPhone #{0}] Tinder: You swiped left. That is correct.", moduleId);
                    Audio.PlaySoundAtTransform("correct", transform);
                    stage++;
                    tinderSetUp();
                    tinderLogic();
                    tinderLog();
                }
                else if (tinderButton == swipeRight && tinderScore >= 1)
                {
                    Debug.LogFormat("[iPhone #{0}] Tinder: You swiped right. That is correct.", moduleId);
                    Audio.PlaySoundAtTransform("correct", transform);
                    stage++;
                    tinderSetUp();
                    tinderLogic();
                    tinderLog();
                }
                else if (tinderScore == 0)
                {
                    if (tinderButton == swipeRight && chosenTinderName.Length >= 5)
                    {
                        Debug.LogFormat("[iPhone #{0}] Tinder: You swiped right. That is correct.", moduleId);
                        Audio.PlaySoundAtTransform("correct", transform);
                        stage++;
                        tinderSetUp();
                        tinderLogic();
                        tinderLog();
                    }
                    else if (tinderButton == swipeLeft && chosenTinderName.Length < 5)
                    {
                        Debug.LogFormat("[iPhone #{0}] Tinder: You swiped left. That is correct.", moduleId);
                        Audio.PlaySoundAtTransform("correct", transform);
                        stage++;
                        tinderSetUp();
                        tinderLogic();
                        tinderLog();
                    }
                    else
                    {
                        GetComponent<KMBombModule>().HandleStrike();
                        strikeCount = Bomb.GetStrikes();
                        Debug.LogFormat("[iPhone #{0}] Tinder: Strike! You pressed {1}. That is incorrect. You have {2} strike(s).", moduleId, tinderButton.name, strikeCount);
                        tinderSetUp();
                        tinderLogic();
                        tinderLog();
                    }
                }
                else
                {
                    GetComponent<KMBombModule>().HandleStrike();
                    strikeCount = Bomb.GetStrikes();
                    Debug.LogFormat("[iPhone #{0}] Tinder: Strike! You pressed {1}. That is incorrect. You have {2} strike(s).", moduleId, tinderButton.name, strikeCount);
                    tinderSetUp();
                    tinderLogic();
                    tinderLog();
                }
                break;

            case 2:
                if (tinderButton == swipeLeft && tinderScore < 0)
                {
                    Debug.LogFormat("[iPhone #{0}] Tinder: You swiped left. That is correct.", moduleId);
                    Audio.PlaySoundAtTransform("correct", transform);
                    stage++;
                    tinderSetUp();
                    tinderLogic();
                    tinderLog();
                }
                else if (tinderButton == swipeRight && tinderScore >= 1)
                {
                    Debug.LogFormat("[iPhone #{0}] Tinder: You swiped right. That is correct.", moduleId);
                    Audio.PlaySoundAtTransform("correct", transform);
                    stage++;
                    tinderSetUp();
                    tinderLogic();
                    tinderLog();
                }
                else if (tinderScore == 0)
                {
                    if (tinderButton == swipeRight && chosenTinderName.Length >= 5)
                    {
                        Debug.LogFormat("[iPhone #{0}] Tinder: You swiped right. That is correct.", moduleId);
                        Audio.PlaySoundAtTransform("correct", transform);
                        stage++;
                        tinderSetUp();
                        tinderLogic();
                        tinderLog();
                    }
                    else if (tinderButton == swipeLeft && chosenTinderName.Length < 5)
                    {
                        Debug.LogFormat("[iPhone #{0}] Tinder: You swiped left. That is correct.", moduleId);
                        Audio.PlaySoundAtTransform("correct", transform);
                        stage++;
                        tinderSetUp();
                        tinderLogic();
                        tinderLog();
                    }
                    else
                    {
                        GetComponent<KMBombModule>().HandleStrike();
                        strikeCount = Bomb.GetStrikes();
                        Debug.LogFormat("[iPhone #{0}] Tinder: Strike! You pressed {1}. That is incorrect. You have {2} strike(s).", moduleId, tinderButton.name, strikeCount);
                        tinderSetUp();
                        tinderLogic();
                        tinderLog();
                        stage = 1;
                    }
                }
                else
                {
                    GetComponent<KMBombModule>().HandleStrike();
                    strikeCount = Bomb.GetStrikes();
                    Debug.LogFormat("[iPhone #{0}] Tinder: Strike! You pressed {1}. That is incorrect. You have {2} strike(s).", moduleId, tinderButton.name, strikeCount);
                    tinderSetUp();
                    tinderLogic();
                    tinderLog();
                    stage = 1;
                }
                break;

            case 3:
                if (tinderButton == swipeLeft && tinderScore < 0)
                {
                    tinderProfile.text = "\nMatch!\n\nThe fourth digit\nof the PIN is\n\n" + pinDigits[3];
                    Debug.LogFormat("[iPhone #{0}] Tinder: You swiped left. That is correct. PIN digit displayed.", moduleId);
                    Audio.PlaySoundAtTransform("correct", transform);
                    tinderDone = true;
                    stage++;
                }
                else if (tinderButton == swipeRight && tinderScore >= 1)
                {
                    tinderProfile.text = "\nMatch!\n\nThe fourth digit\nof the PIN is\n\n" + pinDigits[3];
                    Debug.LogFormat("[iPhone #{0}] Tinder: You swiped right. That is correct. PIN digit displayed.", moduleId);
                    Audio.PlaySoundAtTransform("correct", transform);
                    tinderDone = true;
                    stage++;
                }
                else if (tinderScore == 0)
                {
                    if (tinderButton == swipeRight && chosenTinderName.Length >= 5)
                    {
                        tinderProfile.text = "\nMatch!\n\nThe fourth digit\nof the PIN is\n\n" + pinDigits[3];
                        Debug.LogFormat("[iPhone #{0}] Tinder: You swiped right. That is correct. PIN digit displayed.", moduleId);
                        Audio.PlaySoundAtTransform("correct", transform);
                        tinderDone = true;
                        stage++;
                    }
                    else if (tinderButton == swipeLeft && chosenTinderName.Length < 5)
                    {
                        tinderProfile.text = "\nMatch!\n\nThe fourth digit\nof the PIN is\n\n" + pinDigits[3];
                        Debug.LogFormat("[iPhone #{0}] Tinder: You swiped left. That is correct. PIN digit displayed.", moduleId);
                        Audio.PlaySoundAtTransform("correct", transform);
                        tinderDone = true;
                        stage++;
                    }
                    else
                    {
                        GetComponent<KMBombModule>().HandleStrike();
                        strikeCount = Bomb.GetStrikes();
                        Debug.LogFormat("[iPhone #{0}] Tinder: Strike! You pressed {1}. That is incorrect. You have {2} strike(s).", moduleId, tinderButton.name, strikeCount);
                        tinderSetUp();
                        tinderLogic();
                        stage = 1;
                    }
                }
                else
                {
                    GetComponent<KMBombModule>().HandleStrike();
                    strikeCount = Bomb.GetStrikes();
                    Debug.LogFormat("[iPhone #{0}] Tinder: Strike! You pressed {1}. That is incorrect. You have {2} strike(s).", moduleId, tinderButton.name, strikeCount);
                    tinderSetUp();
                    tinderLogic();
                    tinderLog();
                    stage = 1;
                }
                break;

            default:
                break;
        }
    }

    public void OnPhonePress(string textToAppend)
    {
        Audio.PlaySoundAtTransform("phoneDial", transform);
        photoRight.AddInteractionPunch(.5f);
        if (phoneNumber.text.Length < 9)
            phoneNumber.text += textToAppend;
    }

    public void OnSettingsPress(string textToAppend2)
    {
        Audio.PlaySoundAtTransform("keyClick", transform);
        photoRight.AddInteractionPunch(.5f);
        if (enteredPIN.Length < 5)
        {
            enteredPIN += textToAppend2;
        }
        correctPIN = pinDigits[0] + pinDigits[1] + pinDigits[2] + pinDigits[3];

        switch (settingsStage)
        {
            case 1:
                pinScreen1.material.mainTexture = buttonMat;
                settingsStage++;
                break;

            case 2:
                pinScreen2.material.mainTexture = buttonMat;
                settingsStage++;
                break;

            case 3:
                pinScreen3.material.mainTexture = buttonMat;
                settingsStage++;
                break;

            case 4:
                if (enteredPIN == correctPIN)
                {
                    pinScreen4.material.mainTexture = buttonMat;
                    Audio.PlaySoundAtTransform("factoryReset", transform);
                    Debug.LogFormat("[iPhone #{0}] You entered {1}. That is correct. Module disarmed.", moduleId, enteredPIN);
                    GetComponent<KMBombModule>().HandlePass();
                    solved = "solved";
                    screenStarter();
                    angryBirds.gameObject.SetActive(false);
                    messages.gameObject.SetActive(false);
                    photos.gameObject.SetActive(false);
                    tinder.gameObject.SetActive(false);
                    phone.gameObject.SetActive(false);
                    settings.gameObject.SetActive(false);
                    settingsStage++;
                }
                else
                {
                    GetComponent<KMBombModule>().HandleStrike();
                    strikeCount = Bomb.GetStrikes();
                    Debug.LogFormat("[iPhone #{0}] Strike! You entered {1}. That is incorrect. You have {2} strike(s).", moduleId, enteredPIN, strikeCount);
                    pinScreen1.material.mainTexture = whiteBackground;
                    pinScreen2.material.mainTexture = whiteBackground;
                    pinScreen3.material.mainTexture = whiteBackground;
                    pinScreen4.material.mainTexture = whiteBackground;
                    if (tinderDone == false)
                    {
                        tinderSetUp();
                        tinderLogic();
                    }
                    settingsStage = 1;
                    enteredPIN = "";
                }
                break;

            default:
                GetComponent<KMBombModule>().HandleStrike();
                break;
        }
    }

    public void OnHashButton()
    {
        if (phoneNumber.text == "52716" && (Bomb.GetSerialNumberLetters().Any(x => x == 'A' || x == 'E' || x == 'I' || x == 'O' || x == 'U')))
        {
            GetComponent<KMBombModule>().HandleStrike();
            strikeCount = Bomb.GetStrikes();
            Debug.LogFormat("[iPhone #{0}] Strike! You dialled {1} and solved the Angry Birds app. You have {2} strike(s).", moduleId, phoneNumber.text, strikeCount);
            if (tinderDone == false)
            {
                tinderSetUp();
                tinderLogic();
            }
            angryBirdsWin = "true";
            digitAnswer1.text = pinDigits[0];
            phoneNumber.text = "";
        }
        else if (phoneNumber.text == "43892" && (Bomb.GetSerialNumberLetters().All(x => x != 'A' && x != 'E' && x != 'I' && x != 'O' && x != 'U')))
        {
            GetComponent<KMBombModule>().HandleStrike();
            strikeCount = Bomb.GetStrikes();
            Debug.LogFormat("[iPhone #{0}] Strike! You dialled {1} and solved the Angry Birds app. You have {2} strike(s).", moduleId, phoneNumber.text, strikeCount);
            if (tinderDone == false)
            {
                tinderSetUp();
                tinderLogic();
            }
            angryBirdsWin = "true";
            digitAnswer1.text = pinDigits[0];
            phoneNumber.text = "";
        }
        else if (phoneNumber.text == "60138" && (Bomb.GetSerialNumberLetters().Any(x => x == 'A' || x == 'E' || x == 'I' || x == 'O' || x == 'U')))
        {
            GetComponent<KMBombModule>().HandleStrike();
            strikeCount = Bomb.GetStrikes();
            Debug.LogFormat("[iPhone #{0}] Strike! You dialled {1} and revealed the true message. You have {2} strike(s).", moduleId, phoneNumber.text, strikeCount);
            Audio.PlaySoundAtTransform("messageSFX", transform);
            if (tinderDone == false)
            {
                tinderSetUp();
                tinderLogic();
            }
            messageCheat();
            messageCheatAlert = "true";
            phoneNumber.text = "";
        }
        else if (phoneNumber.text == "15397" && (Bomb.GetSerialNumberLetters().All(x => x != 'A' && x != 'E' && x != 'I' && x != 'O' && x != 'U')))
        {
            GetComponent<KMBombModule>().HandleStrike();
            strikeCount = Bomb.GetStrikes();
            Debug.LogFormat("[iPhone #{0}] Strike! You dialled {1} and revealed the true message. You have {2} strike(s).", moduleId, phoneNumber.text, strikeCount);
            Audio.PlaySoundAtTransform("messageSFX", transform);
            if (tinderDone == false)
            {
                tinderSetUp();
                tinderLogic();
            }
            messageCheat();
            messageCheatAlert = "true";
            phoneNumber.text = "";
        }
        else if (phoneNumber.text == "81606" && (Bomb.GetSerialNumberLetters().Any(x => x == 'A' || x == 'E' || x == 'I' || x == 'O' || x == 'U')))
        {
            GetComponent<KMBombModule>().HandleStrike();
            strikeCount = Bomb.GetStrikes();
            Debug.LogFormat("[iPhone #{0}] Strike! You dialled {1} and revealed the true photo. You have {2} strike(s).", moduleId, phoneNumber.text, strikeCount);
            Audio.PlaySoundAtTransform("keyClick", transform);
            if (tinderDone == false)
            {
                tinderSetUp();
                tinderLogic();
            }
            photoRevealLight = "true";
            phoneNumber.text = "";
        }
        else if (phoneNumber.text == "79431" && (Bomb.GetSerialNumberLetters().All(x => x != 'A' && x != 'E' && x != 'I' && x != 'O' && x != 'U')))
        {
            GetComponent<KMBombModule>().HandleStrike();
            strikeCount = Bomb.GetStrikes();
            Debug.LogFormat("[iPhone #{0}] Strike! You dialled {1} and revealed the true photo. You have {2} strike(s).", moduleId, phoneNumber.text, strikeCount);
            Audio.PlaySoundAtTransform("keyClick", transform);
            if (tinderDone == false)
            {
                tinderSetUp();
                tinderLogic();
            }
            photoRevealLight = "true";
            phoneNumber.text = "";
        }
        else if (phoneNumber.text == "30962" && (Bomb.GetSerialNumberLetters().Any(x => x == 'A' || x == 'E' || x == 'I' || x == 'O' || x == 'U')))
        {
            GetComponent<KMBombModule>().HandleStrike();
            tinderDone = true;
            strikeCount = Bomb.GetStrikes();
            Debug.LogFormat("[iPhone #{0}] Strike! You dialled {1} and matched on Tinder. You have {2} strike(s).", moduleId, phoneNumber.text, strikeCount);
            tinderProfile.text = "\nMatch!\n\nThe fourth digit\nof the PIN is\n\n" + pinDigits[3];
            stage++;
            stage++;
            stage++;
            phoneNumber.text = "";
        }
        else if (phoneNumber.text == "21486" && (Bomb.GetSerialNumberLetters().All(x => x != 'A' && x != 'E' && x != 'I' && x != 'O' && x != 'U')))
        {
            GetComponent<KMBombModule>().HandleStrike();
            tinderDone = true;
            strikeCount = Bomb.GetStrikes();
            Debug.LogFormat("[iPhone #{0}] Strike! You dialled {1} and matched on Tinder. You have {2} strike(s).", moduleId, phoneNumber.text, strikeCount);
            tinderProfile.text = "\nMatch!\n\nThe fourth digit\nof the PIN is\n\n" + pinDigits[3];
            stage++;
            stage++;
            stage++;
            phoneNumber.text = "";
        }
        else
        {
            GetComponent<KMBombModule>().HandleStrike();
            strikeCount = Bomb.GetStrikes();
            Debug.LogFormat("[iPhone #{0}] Strike! You dialled {1}. That number is not recognised. You have {2} strike(s).", moduleId, phoneNumber.text, strikeCount);
            if (tinderDone == false)
            {
                tinderSetUp();
                tinderLogic();
            }
            phoneNumber.text = "";
        }
    }
    void messageCheat()
    {
        if (truthTeller == "Phil")
        {
            philUnread.gameObject.SetActive(true);
        }
        else if (truthTeller == "Mick")
        {
            mickUnread.gameObject.SetActive(true);
        }
        else if (truthTeller == "Rob")
        {
            robUnread.gameObject.SetActive(true);
        }
        else if (truthTeller == "Andy")
        {
            andyUnread.gameObject.SetActive(true);
        }
    }
    private string TwitchHelpMessage = "Select an app by typing !{0} open [bird/messages/photos/tinder/phone/settings]. Select the proper angrybird with !{0} tap TL/TR/BL/BR. Cycle through photos with !{0} cycle. You may also use !{0} left/right # to view a specific photo. Submit Tinder by using swipe [left/right]. Use phone codes at any time with !{0} submit 425631# (Note: This will result in a strike). To submit the factory reset, type !{0} submit 1234";

    private IEnumerator ProcessTwitchCommand(string inputCommand)
    {
        var commands = inputCommand.ToLowerInvariant();
        if (commands == "autosolve")
        {
            StartCoroutine(AutoSolve());
            yield break;
        }

        //Regex conditions for each app
        //Each set of words in parentheses are considered a group. These groups are used later on to match button indicies, to save needing to write them more than once.
        //If there is a ?: in paranthesis, it means that the group will not be captured for later use.
        //However, groups within groups will still be captured, as each set of parentheses are processed individually.
        var appCondition = Regex.Match(commands, "^(?:open |press |tap |select |)(?:(bird|birds)|(messages)|(photos)|(tinder)|(phone)|(settings))$");
        //If the condition in ?(#) matches, then check for this next condition.
        var longBirdCondition = Regex.Match(commands, "^(?:press|tap|select) ((top|bottom|up|down)|(left|right)) ((?(2)(left|right)|(top|bottom|up|down)))$");
        var birdCondition = Regex.Match(commands, "^(?:press|tap|select) (([bt])|([lr]))((?(2)([lr])|([bt])))$");
        var photoCondition = Regex.Match(commands, "^(press |cycle |)(left|right|l|r)( [1-9]|)$");
        var cycleCondition = Regex.Match(commands, "^cycle$");
        var tinderCondition = Regex.Match(commands, "^(press|tap|swipe) (left|right|l|r)$");
        //{1,4} means match between 1 and 4 values of [0-9]
        var submitCondition = Regex.Match(commands, "^(press|submit) ([0-9]{1,4})$");
        //You can submit any code now, hoorah.
        var cheatCondition = Regex.Match(commands, "^(press|submit) ([0-9*]{1,6}#)$");
        var homeCondition = Regex.Match(commands, "^(return|home|back)$");
        
        //Lists of buttons to match index entries
        var apps = new List<KMSelectable> { angryBirds, messages, photos, tinder, phone, settings };
        var birdSelectables = new[] { TopLeft, TopRight, BottomLeft, BottomRight };
        var photoSelectables = new List<KMSelectable> { photoLeft, photoRight };
        var tinderSelectables = new List<KMSelectable> { swipeLeft, swipeRight };
        var phoneSelectables = new List<KMSelectable> { zeroButton, oneButton, twoButton, threeButton, fourButton, fiveButton, sixButton, sevenButton,
        eightButton, nineButton, starButton, hashButton };
        var solveSelectables = new List<KMSelectable> { zeroSButton, oneSButton, twoSButton, threeSButton, fourSButton,
        fiveSButton, sixSButton, sevenSButton, eightSButton, nineSButton };
        //Due to variety, bird commands are going to need a few extra cases
        var birdArea = new List<char> { 't', 'b', 'l', 'r' };
        //For photos and tinder
        var selectableChar = new List<char> { 'l', 'r' };

        //Since only one of these can be true at a time, use IndexOf(true) rather than using each Regex.Success separately
        var matchedCondition = new List<bool> { birdCondition.Success || longBirdCondition.Success, false,
         photoCondition.Success, tinderCondition.Success, cheatCondition.Success, submitCondition.Success };
        //List of keeping track which screen we're on. Also used to make sure people aren't submitting commands to the incorrect screen
        var curScreen = new List<bool> { TopLeft.gameObject.activeInHierarchy, philMessage.gameObject.activeInHierarchy, photoLeft.gameObject.activeInHierarchy,
            tinderProfile.gameObject.activeInHierarchy, oneButton.gameObject.activeInHierarchy, oneSButton.gameObject.activeInHierarchy, angryBirds.gameObject.activeInHierarchy };

        //Press home if expecting a home press, or attempting to open an application from another screen, or when expecting to cycle.
        //There's no need to do it if already on home screen. If the desired screen matches the current screen, there's no need to press home. And if cycling, there's no need to press home when already cycling.
        var homeBool = !curScreen.Last() && (homeCondition.Success || (appCondition.Success && !appCondition.Groups[curScreen.IndexOf(true) + 2].Success) || (cycleCondition.Success && !curScreen[2]));
        //Press home when submitting numbers and not in the settings/phone apps.
        homeBool = homeBool || (matchedCondition[4] && !curScreen[4]) || (matchedCondition[5] && !curScreen[5]);

        var list = new List<KMSelectable>();
        var move = 1;
        //Press the home button if either opening an app or returning to the home screen.
        if (homeBool)
            list.Add(home);

        //Outside of the home press, process cycleCondition separately
        if (cycleCondition.Success)
        {
            yield return null;
            if (!curScreen[2]) list.Add(apps[2]);
            if (list.Count > 0) yield return list.ToArray();
            yield return new WaitForSeconds(1);
            for (int pic = 0; pic < 8; pic++)
            {
                //Basically tells tp to wait 2 seconds unless someone sends !cancel
                yield return "trywaitcancel 2 The cycle has been aborted due to a request to cancel";
                yield return new KMSelectable[] { photoRight };
            }
            yield break;
        }

        //If homeBool is true, this means the home button will be pressed, and we will be in the home screen.
        //If curScreen.Last() is true, this means we were already on the home screen.
        if (appCondition.Success && (homeBool || curScreen.Last()))
        {
            for (int i = 0; i < apps.Count(); i++)
            {
                //Check each group for a match within a match so that I don't need to make switch statements
                //Start at 1, as 0 is the whole match.
                if (appCondition.Groups[i + 1].Success)
                {
                    list.Add(apps[i]);
                    break;
                }
            }
        }
        //birdCondition || longBirdCondition && angry birds screen
        if (matchedCondition[0] && curScreen[0])
        {
            var birdPress = birdCondition.Success ? birdCondition : longBirdCondition;
            //grab the matched values from birdCondition/longBirdConditon, and replace instances of up/down with t/b
            var ud0 = birdPress.Groups[1].Value.Replace("u", "t").Replace("d", "b");
            var ud1 = birdPress.Groups[4].Value.Replace("u", "t").Replace("d", "b");
            //Fancy math to get index values that work for the button presses.
            //Each value is added by 1, because 0 multiplied by anything is always 0.
            //The resulting values are 3, 4, 6, and 8. Dividing all of these numbers by 2 gets me 1, 2, 3, 4, which gets me values perfectly compatible with indexing.
            //As far as integer division goes anyway, that was just a stroke of luck.
            var chars = ((birdArea.IndexOf(ud0[0]) + 1) * (birdArea.IndexOf(ud1[0]) + 1) / 2) - 1;
            list.Add(birdSelectables[chars]);
        }
        //photoCondition && photos screen
        if (matchedCondition[2] && curScreen[2])
        {
            //check only l/r rather than l/r and left/right
            var dir = photoCondition.Groups[2].Value[0];
            var str = photoCondition.Groups[3].Value;
            //calling selectables[dir] should call the selectable directly.
            //check "#" vs " #". Return -1 if not a number.
            move = str == "" ? 1 : str.Length > 1 ? str[1] - '0' : -1;
            if (move > 7 || move < 1)
            {
                yield return "sendtochat Please submit a number between 1 and 7.";
                yield break;
            }
            for (int i = 0; i < move; i++)
                list.Add(photoSelectables[selectableChar.IndexOf(dir)]);
        }
        //tinderCondition && tinder screen
        if (matchedCondition[3] && curScreen[3])
        {
            //Basically the same method as photos, but without numbers or cycling.
            var swipeDir = tinderCondition.Groups[2].Value[0];
            list.Add(tinderSelectables[selectableChar.IndexOf(swipeDir)]);
        }
        if (matchedCondition[4] || matchedCondition[5])
        {
            //Enter either settings app or phone app based on which matched condition is true.
            var matched = matchedCondition.IndexOf(true);
            if (homeBool) list.Add(apps[matched]);
            var code = matched == 4 ? cheatCondition.Groups[2].Value : submitCondition.Groups[2].Value;
            var buttons = matched == 4 ? phoneSelectables : solveSelectables;
            for (int i = 0; i < code.Length; i++)
            {
                var index = code[i] == '*' ? 10 : code[i] == '#' ? 11 : code[i] - '0';
                list.Add(buttons[index]);
            }
        }
        if (list.Count == 0) yield break;
        yield return null;
        yield return list.ToArray();
    }
    
    private IEnumerator TwitchHandleForcedSolve()
    {
        StartCoroutine(AutoSolve());
        yield return null;
    }

    private IEnumerator AutoSolve()
    {
        var pinScreens = new[] { pinScreen1, pinScreen2, pinScreen3, pinScreen4 };
        var birdDictionary = new Dictionary<string, KMSelectable> { { "Top Left", TopLeft }, { "Top Right", TopRight }, { "Bottom Left", BottomLeft }, { "Bottom Right", BottomRight } };
        foreach (MeshRenderer mesh in pinScreens)
            mesh.material.mainTexture = whiteBackground;
        var coroutine = CheatPress(0);
        settingsStage = 1;
        enteredPIN = "";
        phoneNumber.text = "";
        if (!angryBirds.gameObject.activeInHierarchy)
        {
            home.OnInteract();
            yield return null;
        }
        if (angryBirdsWin != "true")
        {
            while (coroutine.MoveNext())
                yield return coroutine.Current;
            angryBirds.OnInteract();
            yield return new WaitForSeconds(0.3f);
            birdDictionary[correctAngryBird].OnInteract();
            yield return new WaitForSeconds(0.3f);
            home.OnInteract();
            yield return null;
        }
        coroutine = CheatPress(1);
        while (coroutine.MoveNext())
            yield return coroutine.Current;
        var unread = new[] { philUnread, mickUnread, robUnread, andyUnread };
        var unreadNames = new[] { "Phil", "Mick", "Rob", "Andy" };
        if (messageCheatAlert != "true")
        {
            unread[Array.IndexOf(unreadNames, truthTeller)].gameObject.SetActive(true);
            messageCheatAlert = "true";
            messages.OnInteract();
            yield return new WaitForSeconds(0.3f);
            home.OnInteract();
            yield return null;
        }
        coroutine = CheatPress(2);
        while (coroutine.MoveNext())
            yield return coroutine.Current;
        if (photoRevealLight != "true")
        {
            photos.OnInteract();
            yield return null;
            photoRevealLight = "true";
            while (photoScreen.material.mainTexture != truePhotos[truePhotosSelection])
            {
                photoRight.OnInteract();
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.2f);
            home.OnInteract();
            yield return null;
        }
        if (!tinderDone)
        {
            stage = 1;
            coroutine = CheatPress(3);
            while (coroutine.MoveNext())
                yield return coroutine.Current;
            tinder.OnInteract();
            yield return new WaitForSeconds(0.3f);
            while (!tinderDone)
            {
                if (tinderScore < 0 || (tinderScore == 0 && chosenTinderName.Length < 5))
                    swipeLeft.OnInteract();
                else if (tinderScore >= 1 || (tinderScore == 0 && chosenTinderName.Length >= 5))
                    swipeRight.OnInteract();
                yield return new WaitForSeconds(0.3f);
            }
            home.OnInteract();
            yield return null;
        }
        settings.OnInteract();
        yield return new WaitForSeconds(0.1f);
        foreach (string s in pinDigits)
        {
            OnSettingsPress(s);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator CheatPress(int num)
    {
        var vowel = !Bomb.GetSerialNumberLetters().All(x => x != 'A' && x != 'E' && x != 'I' && x != 'O' && x != 'U');
        var codeDictionaries = new[] {
            new Dictionary<bool, int> { { true, 52716 }, { false, 43892 } },
            new Dictionary<bool, int> { { true, 60138 }, { false, 15397 } },
            new Dictionary<bool, int> { { true, 81606 }, { false, 79431 } },
            new Dictionary<bool, int> { { true, 30962 }, { false, 21486 } }
        };
        phone.OnInteract();
        yield return null;
        foreach (char c in codeDictionaries[num][vowel].ToString())
        {
            //Do OnPhonePress rather than press each individual button for ease
            OnPhonePress(c.ToString());
            yield return new WaitForSeconds(0.1f);
        }
        home.OnInteract();
        phoneNumber.text = "";
        yield return null;
    }
}
