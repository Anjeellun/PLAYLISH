using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LanguageSwitcher : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public Button languageButton;
    public int weatherIndex; 
    private bool isEnglish = true;
    private bool languageToggled = false;

    private string[] englishTexts = new string[]
    {
        // Foggy
        "Foggy weather happens when tiny water droplets float in the air, making it hard to see far away. It’s like the clouds have come down to visit the ground. Fog often appears in the early morning, especially near mountains, lakes, or rivers. People need to be extra careful when driving or walking because the world looks blurry in the fog. Sometimes, the fog can feel cool and damp on your skin.",
        // Hail
        "Hail happens when rain turns into ice balls in the clouds because of strong, cold winds high in the sky. These icy balls, called hailstones, fall to the ground, making loud noises when they hit roofs, cars, or the ground. Hailstones can be small like peas or big like golf balls. Hailstorms don’t last long, but they can sometimes damage plants, cars, or even windows.",
        // Hot
        "Hot weather is when the sun shines brightly, making the air feel warm or very hot. You might sweat a lot and feel thirsty on hot days. People wear light, comfortable clothes, drink lots of water, and stay in shady places to cool down. In very hot weather, the ground can get so warm that walking barefoot might hurt your feet. Beaches and swimming pools are popular places to visit when it’s hot.",
        // Snowy
        "Snowy weather happens when the air is so cold that water droplets in the air freeze into tiny snowflakes. Snowflakes are unique, like small icy stars, and no two snowflakes look exactly the same. When snow falls, it creates a soft white blanket over the ground, trees, and rooftops. Kids love to play in the snow by making snowmen or throwing snowballs. This weather happens in cold places or during winter.",
        // Thunder
        "Thunder is the loud, rumbling sound we hear during storms. It happens when lightning flashes through the sky, heating the air around it super quickly. The air expands and creates the roaring sound of thunder. Even though thunder is loud, it can’t hurt you, it’s just a sound. But it usually means a storm is nearby, so it’s best to stay inside where it’s safe.",
        // Stormy
        "Stormy weather brings strong winds, heavy rain, and sometimes thunder and lightning. The sky turns dark, and the wind can howl loudly. Storms can knock down tree branches or even cause power outages. People usually stay indoors during a storm to keep safe. After the storm, the air often feels fresh, and you might even see a rainbow.",
        // Cold
        "Cold weather happens when the temperature drops, making the air feel chilly. People wear warm clothes like jackets, scarves, and gloves to stay cozy. On cold days, you might see your breath when you breathe out, it looks like a little puff of smoke. Cold weather is common in winter or at night in high places, like mountains.",
        // Windy
        "Windy weather is when the air moves fast, pushing leaves, branches, and sometimes even hats. On gentle windy days, you can fly kites or feel the cool breeze on your face. But on very windy days, the wind can make it hard to walk or even knock things over. Wind is strongest near oceans or in wide, open areas.",
        // Rainy
        "Rainy weather is when water falls from clouds as raindrops. Raindrops help plants grow, fill rivers and lakes, and make the earth cool and fresh. When it rains lightly, it feels calm and soothing, but heavy rain can cause puddles and make everything wet. People use umbrellas, raincoats, or boots to stay dry. The sound of rain hitting the roof or windows is relaxing to listen to.",
        // Drizzle
        "Drizzle is light rain that feels soft and gentle, like a mist falling from the sky. It’s not as heavy as regular rain, so you might only feel small droplets on your face or clothes. On drizzly days, the sky looks grey, and the air feels moist. It’s a peaceful kind of weather that often happens before or after heavier rain.",
        // Overcast
        "Overcast weather is when the sky is covered with thick grey clouds, hiding the sun. Even though it looks like it might rain, sometimes it doesn’t. The day feels cooler and a little gloomy, but overcast skies are great for taking walks because the sun isn’t too bright. It’s like the sky is wearing a big grey blanket.",
        // Sunny
        "Sunny weather is when the sun is shining bright, and the sky is clear blue with few or no clouds. It’s warm and cheerful, making it perfect for outdoor activities like playing in the park, going to the beach, or riding a bike. Don’t forget to wear sunscreen to protect your skin from the sun’s rays and drink lots of water to stay hydrated."
    };

    private string[] indonesianTexts = new string[]
    {
        // Berkabut
        "Cuaca berkabut terjadi ketika tetesan air kecil melayang di udara, sehingga sulit untuk melihat jauh. Rasanya seperti awan turun ke tanah. Kabut sering muncul di pagi hari, terutama di dekat pegunungan, danau, atau sungai. Orang-orang harus lebih berhati-hati saat berkendara atau berjalan karena semuanya terlihat buram dalam kabut. Terkadang, kabut terasa sejuk dan lembap di kulit.",
        // Hujan Es
        "Hujan es terjadi ketika air hujan berubah menjadi bola-bola es di dalam awan karena angin dingin yang kuat di langit. Bola-bola es ini disebut butiran es (hailstones), dan jatuh ke tanah sambil mengeluarkan suara keras saat mengenai atap, mobil, atau tanah. Butiran es bisa sekecil kacang polong atau sebesar bola golf. Hujan es biasanya tidak berlangsung lama, tetapi kadang-kadang bisa merusak tanaman, mobil, atau bahkan jendela.",
        // Panas
        "Cuaca panas terjadi ketika matahari bersinar terik, membuat udara terasa hangat atau sangat panas. Kamu mungkin akan banyak berkeringat dan merasa haus di hari yang panas. Orang-orang biasanya memakai pakaian tipis dan nyaman, minum banyak air, dan mencari tempat teduh untuk mendinginkan diri. Di cuaca yang sangat panas, tanah bisa begitu hangat hingga berjalan tanpa alas kaki dapat melukai kaki. Pantai dan kolam renang sering menjadi tempat favorit saat cuaca panas.",
        // Bersalju
        "Cuaca bersalju terjadi ketika udara sangat dingin sehingga tetesan air di udara membeku menjadi kepingan salju. Kepingan salju ini unik, seperti bintang kecil yang membeku, dan tidak ada dua kepingan salju yang persis sama. Saat salju turun, ia menciptakan selimut putih lembut di tanah, pohon, dan atap. Anak-anak suka bermain salju dengan membuat manusia salju atau melempar bola salju. Cuaca ini terjadi di tempat dingin atau saat musim dingin.",
        // Guntur
        "Guntur adalah suara gemuruh yang keras yang kita dengar saat badai. Guntur terjadi ketika kilat menyambar langit, memanaskan udara di sekitarnya dengan sangat cepat. Udara ini mengembang dan menciptakan suara gemuruh guntur. Meskipun guntur terdengar keras, suara ini tidak bisa melukai, itu hanya suara. Namun, biasanya guntur berarti badai sedang mendekat, jadi lebih baik tetap di dalam rumah untuk aman.",
        // Badai
        "Cuaca badai membawa angin kencang, hujan lebat, dan terkadang kilat serta guntur. Langit menjadi gelap, dan angin bisa berdesir dengan keras. Badai bisa merobohkan dahan pohon atau bahkan menyebabkan listrik padam. Orang-orang biasanya tetap di dalam rumah saat badai untuk tetap aman. Setelah badai berlalu, udara sering terasa segar, dan terkadang muncul pelangi.",
        // Dingin
        "Cuaca dingin terjadi ketika suhu udara turun, membuat udara terasa sejuk atau sangat dingin. Orang-orang memakai pakaian hangat seperti jaket, syal, dan sarung tangan untuk tetap nyaman. Di hari yang dingin, kamu mungkin bisa melihat napasmu sendiri saat bernapas keluar, terlihat seperti asap kecil. Cuaca dingin sering terjadi di musim dingin atau di malam hari di tempat tinggi seperti pegunungan.",
        // Berangin
        "Cuaca berangin terjadi ketika udara bergerak cepat, mendorong daun, ranting, dan bahkan kadang-kadang topi. Di hari yang berangin lembut, kamu bisa menerbangkan layang-layang atau merasakan angin sejuk di wajahmu. Tetapi di hari yang sangat berangin, angin bisa membuatmu sulit berjalan atau bahkan menjatuhkan benda-benda. Angin paling kencang biasanya terjadi di dekat laut atau di tempat yang luas dan terbuka.",
        // Hujan
        "Cuaca hujan terjadi ketika air jatuh dari awan dalam bentuk tetesan. Tetesan hujan membantu tanaman tumbuh, mengisi sungai dan danau, serta membuat bumi menjadi sejuk dan segar. Ketika hujan turun ringan, rasanya tenang dan menenangkan, tetapi hujan deras bisa membuat genangan air dan membasahi segala sesuatu. Orang-orang menggunakan payung, jas hujan, atau sepatu bot untuk tetap kering. Suara hujan yang jatuh di atap atau jendela sangat menyenangkan untuk didengar.",
        // Gerimis
        "Gerimis adalah hujan ringan yang terasa lembut dan pelan, seperti kabut yang jatuh dari langit. Tidak seberat hujan biasa, jadi kamu mungkin hanya merasakan tetesan kecil di wajah atau pakaianmu. Di hari gerimis, langit terlihat abu-abu, dan udara terasa lembap. Gerimis sering terjadi sebelum atau setelah hujan deras.",
        // Mendung
        "Cuaca mendung terjadi ketika langit tertutup awan tebal berwarna abu-abu, sehingga matahari tidak terlihat. Meskipun terlihat seperti akan hujan, kadang-kadang tidak jadi. Hari terasa lebih sejuk dan sedikit suram, tetapi langit mendung cocok untuk berjalan-jalan karena matahari tidak terlalu terang. Rasanya seperti langit sedang memakai selimut abu-abu besar.",
        // Cerah
        "Cuaca cerah terjadi ketika matahari bersinar terang dan langit berwarna biru tanpa banyak awan. Cuaca ini terasa hangat dan ceria, sangat cocok untuk kegiatan di luar ruangan seperti bermain di taman, pergi ke pantai, atau bersepeda. Jangan lupa memakai tabir surya untuk melindungi kulitmu dari sinar matahari dan minum banyak air agar tetap segar."
    };

    void Start()
    {
        languageButton.onClick.AddListener(ToggleLanguage);
        UpdateDescription();
    }

    void ToggleLanguage()
    {
        isEnglish = !isEnglish;
        languageToggled = isEnglish == false;
        UpdateDescription();
    }

    public void ResetToEnglish()
    {
        isEnglish = true;
        languageToggled = false;
        UpdateDescription();
    }

    void UpdateDescription()
    {
        if (isEnglish)
            descriptionText.text = englishTexts[weatherIndex];
        else
            descriptionText.text = indonesianTexts[weatherIndex];
    }

    void OnDisable()
    {
        isEnglish = true;
        UpdateDescription();
    }
}
