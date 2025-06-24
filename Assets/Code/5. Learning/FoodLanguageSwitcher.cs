using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FoodLanguageSwitcher : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public Button languageButton;
    public int foodIndex;
    private bool isEnglish = true;

    private string[] englishTexts = new string[]
    {
        // 1. Burger
        "A burger is a round sandwich with a meat patty inside a soft bun. It usually has lettuce, tomato, cheese, and sauce. Burgers are often eaten for lunch or dinner. You can hold a burger with your hands and take big bites. Sometimes, burgers come with fries and a drink.",
        // 2. Pancake
        "Pancakes are flat, round cakes made from flour, eggs, and milk. They are cooked on a pan and are soft and fluffy. People eat pancakes with honey, syrup, or fruit. Pancakes are often eaten for breakfast.",
        // 3. Salad
        "Salad is a mix of fresh vegetables like lettuce, tomato, and cucumber. Some salads have fruit, cheese, or eggs. Salad is crunchy and healthy, and people often eat it before the main meal.",
        // 4. Bread
        "Bread is a soft and fluffy food made from flour, water, and yeast. People often eat bread for breakfast or as a snack. You can enjoy it plain, with butter, or with sweet jam. Bread can be sliced or shaped into rolls. It is easy to carry and fills your tummy.",
        // 5. Cake
        "Cake is a sweet dessert made from flour, eggs, sugar, and butter. Cakes are soft and fluffy, and often have cream or chocolate on top. People eat cake during birthdays or celebrations. Each slice of cake can have different flavors like chocolate, vanilla, or fruit.",
        // 6. Candy
        "Candy is a small, sweet treat that comes in many colors and shapes. Some candies are chewy, while others are hard or melt in your mouth. Kids like to eat candy as a treat after meals or during special days. Candy can taste fruity, minty, or chocolaty.",
        // 7. Cheese
        "Cheese is made from milk and has a creamy, salty taste. It can be sliced, grated, or melted. People eat cheese with bread, on pizza, or as a snack. Cheese comes in many types, some are soft and some are hard.",
        // 8. Chicken Nugget
        "Chicken nuggets are small pieces of chicken covered in crispy bread crumbs. They are golden brown and taste savory. Nuggets are easy to eat with your fingers and are often dipped in ketchup or sauce. Many kids like chicken nuggets for lunch or as a snack.",
        // 9. Chocolate
        "Chocolate is a sweet food made from cocoa beans. It is smooth and melts in your mouth. Chocolate can be dark, milk, or white. People eat chocolate as a treat, and it is also used in cakes and cookies.",
        // 10. Coffee
        "Coffee is a warm drink made from roasted coffee beans. Adults often drink coffee in the morning to feel awake. Coffee can be black or mixed with milk and sugar. It has a strong smell and a slightly bitter taste.",
        // 11. Cupcake
        "A cupcake is a small, round cake baked in a paper cup. Cupcakes are soft and sweet, often topped with colorful icing and sprinkles. They are easy to hold and eat. Cupcakes are popular at parties and celebrations.",
        // 12. Donut
        "A donut is a round, sweet bread with a hole in the middle. Donuts are soft and sometimes covered with sugar, chocolate, or colorful icing. People eat donuts for breakfast or as a snack. Donuts can have fillings like jam or cream inside.",
        // 13. Boiled Egg
        "A boiled egg is an egg cooked in hot water until the white and yolk become firm. Boiled eggs are easy to peel and eat. People often eat boiled eggs for breakfast or as a healthy snack. Boiled eggs are full of protein and help your body grow strong.",
        // 14. French Fries
        "French fries are thin pieces of potato that are fried until crispy and golden. They taste salty and are often eaten with ketchup. French fries are a popular side dish with burgers or fried chicken.",
        // 15. Fried Chicken
        "Fried chicken is chicken meat covered in crunchy batter and fried until golden brown. The outside is crispy and the inside is juicy. Fried chicken is often eaten with rice or fries. Many kids enjoy eating fried chicken at parties.",
        // 16. Fried Egg
        "A fried egg is cooked on a pan with a little oil. The white part becomes firm while the yellow yolk stays soft. Fried eggs are often eaten for breakfast with rice or bread.",
        // 17. Fried Rice
        "Fried rice is rice cooked with vegetables, eggs, and sometimes meat or shrimp. It is colorful and full of flavor. Fried rice is often eaten for breakfast or lunch. People sometimes add soy sauce or chili for extra taste.",
        // 18. Ice Cream
        "Ice cream is a cold, sweet dessert made from milk and sugar. It is soft and melts in your mouth. Ice cream comes in many flavors like chocolate, vanilla, and strawberry. People eat ice cream in a cone or a cup, especially on hot days.",
        // 19. Instant Noodles
        "Instant noodles are thin noodles that cook quickly in hot water. They come in a packet or cup with seasoning. Instant noodles are soft and tasty, and people eat them for a quick meal. You can add vegetables or eggs to make them healthier.",
        // 20. Lemonade
        "Lemonade is a cool drink made from lemon juice, water, and sugar. It tastes sweet and a little sour. Lemonade is refreshing and perfect for hot weather. People sometimes add ice cubes to make it colder.",
        // 21. Meatball
        "Meatballs are small, round balls made from ground meat. They are soft and tasty, often served in soup or with noodles. Meatballs can be made from beef, chicken, or fish.",
        // 22. Milk
        "Milk is a white drink that comes from cows or other animals. Milk is full of calcium, which helps your bones grow strong. People drink milk for breakfast or before bed. Milk can also be used in cereal or to make desserts.",
        // 23. Mineral Water
        "Mineral water is clean water that comes from natural springs. It has important minerals that are good for your body. Drinking mineral water helps keep you healthy and hydrated.",
        // 24. Muffin
        "A muffin is a small, round cake that is soft and sweet. Muffins can have chocolate chips, fruit, or nuts inside. People eat muffins for breakfast or as a snack. Muffins are baked in special cups and are easy to carry.",
        // 25. Sandwich
        "A sandwich is made by putting meat, cheese, or vegetables between two slices of bread. Sandwiches are easy to hold and eat. People bring sandwiches to school or picnics. You can make many kinds of sandwiches with different fillings.",
        // 26. Satay
        "Satay is small pieces of meat grilled on a stick. The meat is often covered with a tasty peanut sauce. Satay is popular at parties and is usually eaten with rice cakes or cucumber.",
        // 27. Sausage
        "Sausage is a long, round food made from ground meat and spices. Sausages can be grilled, fried, or boiled. People eat sausages with rice, bread, or noodles.",
        // 28. Soup
        "Soup is a warm, watery food with vegetables, meat, or noodles inside. Soup is comforting when you feel cold or unwell. People eat soup with a spoon, and sometimes add crackers or bread.",
        // 29. Steak
        "Steak is a thick slice of meat, usually beef, cooked until it is juicy and tender. People eat steak with vegetables or potatoes. Steak is often served during special meals or celebrations.",
        // 30. Rice (White Rice)
        "White rice is a small, white grain that becomes soft and fluffy when cooked. White rice is a main food in many countries. People eat white rice with vegetables, meat, or fish. White rice gives you energy to play and learn."
    };

    private string[] indonesianTexts = new string[]
    {
        // 1. Burger
        "Burger adalah roti bundar yang di dalamnya ada daging. Biasanya ada selada, tomat, keju, dan saus juga. Burger sering dimakan saat makan siang atau malam. Burger bisa dipegang dengan tangan dan digigit besar-besar. Kadang burger disajikan bersama kentang goreng dan minuman.",
        // 2. Pancake
        "Pancake adalah kue pipih bulat yang terbuat dari tepung, telur, dan susu. Pancake dimasak di atas wajan dan teksturnya lembut. Orang makan pancake dengan madu, sirup, atau buah. Pancake sering dimakan saat sarapan.",
        // 3. Salad
        "Salad adalah campuran sayur segar seperti selada, tomat, dan mentimun. Ada juga salad yang berisi buah, keju, atau telur. Salad renyah dan sehat, sering dimakan sebelum makan utama.",
        // 4. Roti
        "Roti adalah makanan lembut dan empuk yang terbuat dari tepung, air, dan ragi. Orang sering makan roti saat sarapan atau sebagai camilan. Roti bisa dimakan polos, dengan mentega, atau dengan selai manis. Roti bisa dipotong-potong atau dibentuk bulat. Roti mudah dibawa dan membuat perut kenyang.",
        // 5. Kue
        "Kue adalah makanan penutup manis yang terbuat dari tepung, telur, gula, dan mentega. Kue biasanya lembut dan sering diberi krim atau cokelat di atasnya. Orang makan kue saat ulang tahun atau acara spesial. Setiap potong kue bisa punya rasa berbeda seperti cokelat, vanila, atau buah.",
        // 6. Permen
        "Permen adalah makanan kecil yang manis dan berwarna-warni. Ada permen yang kenyal, keras, atau langsung meleleh di mulut. Anak-anak suka makan permen sebagai hadiah setelah makan atau di hari spesial. Permen bisa terasa buah, mint, atau cokelat.",
        // 7. Keju
        "Keju terbuat dari susu dan rasanya gurih serta sedikit asin. Keju bisa dipotong, diparut, atau dilelehkan. Orang makan keju dengan roti, di atas pizza, atau sebagai camilan. Ada banyak jenis keju, ada yang lembut dan ada yang keras.",
        // 8. Nugget Ayam
        "Nugget ayam adalah potongan ayam kecil yang dibalut tepung roti renyah. Warnanya keemasan dan rasanya gurih. Nugget mudah dimakan dengan tangan dan sering dicelup ke saus tomat atau saus lain. Banyak anak suka makan nugget ayam saat makan siang atau camilan.",
        // 9. Cokelat
        "Cokelat adalah makanan manis yang terbuat dari biji kakao. Cokelat lembut dan meleleh di mulut. Ada cokelat hitam, susu, atau putih. Orang makan cokelat sebagai camilan, dan cokelat juga dipakai untuk membuat kue dan biskuit.",
        // 10. Kopi
        "Kopi adalah minuman hangat yang dibuat dari biji kopi yang disangrai. Orang dewasa sering minum kopi di pagi hari agar lebih segar. Kopi bisa diminum hitam atau dicampur susu dan gula. Aromanya kuat dan rasanya sedikit pahit.",
        // 11. Cupcake
        "Cupcake adalah kue kecil bulat yang dipanggang dalam cup kertas. Cupcake lembut dan manis, sering dihias dengan krim warna-warni dan taburan. Cupcake mudah dipegang dan dimakan. Cupcake sering ada di pesta atau acara spesial.",
        // 12. Donat
        "Donat adalah roti manis berbentuk bulat dengan lubang di tengah. Donat lembut dan kadang diberi gula, cokelat, atau krim warna-warni di atasnya. Orang makan donat saat sarapan atau camilan. Ada donat yang berisi selai atau krim di dalamnya.",
        // 13. Telur Rebus
        "Telur rebus adalah telur yang dimasak dalam air panas sampai bagian putih dan kuningnya menjadi padat. Telur rebus mudah dikupas dan dimakan. Orang sering makan telur rebus saat sarapan atau sebagai camilan sehat. Telur rebus kaya protein dan membantu tubuh tumbuh kuat.",
        // 14. Kentang Goreng
        "Kentang goreng adalah potongan kentang tipis yang digoreng sampai renyah dan berwarna keemasan. Rasanya asin dan sering dimakan dengan saus tomat. Kentang goreng sering jadi teman makan burger atau ayam goreng.",
        // 15. Ayam Goreng
        "Ayam goreng adalah daging ayam yang dibalut tepung dan digoreng sampai renyah. Bagian luar ayam garing dan bagian dalamnya lembut. Ayam goreng sering dimakan dengan nasi atau kentang goreng. Banyak anak suka makan ayam goreng di acara ulang tahun.",
        // 16. Telur Mata Sapi
        "Telur mata sapi dimasak di atas wajan dengan sedikit minyak. Bagian putih telur menjadi padat dan kuningnya tetap lembut. Telur mata sapi sering dimakan saat sarapan bersama nasi atau roti.",
        // 17. Nasi Goreng
        "Nasi goreng adalah nasi yang dimasak bersama sayur, telur, dan kadang daging atau udang. Warnanya cerah dan rasanya enak. Nasi goreng sering dimakan saat sarapan atau makan siang. Kadang orang menambah kecap atau cabai agar lebih lezat.",
        // 18. Es Krim
        "Es krim adalah makanan penutup dingin dan manis yang terbuat dari susu dan gula. Es krim lembut dan meleleh di mulut. Ada banyak rasa es krim seperti cokelat, vanila, dan stroberi. Orang makan es krim di cone atau cup, apalagi saat cuaca panas.",
        // 19. Mie Instan
        "Mie instan adalah mie tipis yang cepat matang dengan air panas. Mie instan ada di bungkus atau cup bersama bumbu. Mie instan lembut dan enak, cocok untuk makan cepat. Kamu bisa menambah sayur atau telur agar lebih sehat.",
        // 20. Lemonade
        "Lemonade adalah minuman dingin dari air perasan lemon, air, dan gula. Rasanya manis dan sedikit asam. Lemonade segar dan cocok diminum saat cuaca panas. Kadang orang menambah es batu agar lebih dingin.",
        // 21. Bakso
        "Bakso adalah bola kecil dari daging yang digiling. Bakso lembut dan enak, sering disajikan dalam kuah atau bersama mie. Bakso bisa dibuat dari daging sapi, ayam, atau ikan.",
        // 22. Susu
        "Susu adalah minuman putih yang berasal dari sapi atau hewan lain. Susu kaya kalsium yang membantu tulang tumbuh kuat. Orang minum susu saat sarapan atau sebelum tidur. Susu juga bisa dicampur sereal atau untuk membuat makanan penutup.",
        // 23. Air Mineral
        "Air mineral adalah air bersih yang berasal dari mata air alami. Air mineral mengandung mineral penting yang baik untuk tubuh. Minum air mineral membantu tubuh tetap sehat dan segar.",
        // 24. Muffin
        "Muffin adalah kue kecil bulat yang lembut dan manis. Muffin bisa berisi cokelat, buah, atau kacang di dalamnya. Orang makan muffin saat sarapan atau sebagai camilan. Muffin dipanggang dalam cup khusus dan mudah dibawa.",
        // 25. Sandwich
        "Sandwich dibuat dengan daging, keju, atau sayur di antara dua potong roti. Sandwich mudah dipegang dan dimakan. Orang membawa sandwich ke sekolah atau piknik. Sandwich bisa dibuat dengan banyak isi yang berbeda.",
        // 26. Sate
        "Sate adalah potongan daging kecil yang dipanggang di tusuk. Daging sate biasanya diberi saus kacang yang gurih. Sate populer di pesta dan biasanya dimakan dengan lontong atau mentimun.",
        // 27. Sosis
        "Sosis adalah makanan panjang bulat dari daging yang digiling dan diberi bumbu. Sosis bisa dipanggang, digoreng, atau direbus. Orang makan sosis dengan nasi, roti, atau mie.",
        // 28. Sup
        "Sup adalah makanan berkuah hangat yang berisi sayur, daging, atau mie. Sup enak dimakan saat cuaca dingin atau saat kurang sehat. Orang makan sup dengan sendok, kadang ditambah kerupuk atau roti.",
        // 29. Steak
        "Steak adalah potongan daging tebal, biasanya daging sapi, yang dimasak sampai empuk dan juicy. Orang makan steak dengan sayur atau kentang. Steak sering disajikan saat acara spesial atau makan bersama keluarga.",
        // 30. Nasi Putih
        "Nasi putih adalah butiran kecil berwarna putih yang menjadi lembut dan empuk setelah dimasak. Nasi putih adalah makanan utama di banyak negara. Orang makan nasi putih bersama sayur, daging, atau ikan. Nasi putih memberi energi untuk bermain dan belajar."
    };

    void Start()
    {
        languageButton.onClick.AddListener(ToggleLanguage);
        UpdateDescription();
    }

    void ToggleLanguage()
    {
        isEnglish = !isEnglish;
        UpdateDescription();
    }

    void UpdateDescription()
    {
        if (isEnglish)
            descriptionText.text = englishTexts[foodIndex];
        else
            descriptionText.text = indonesianTexts[foodIndex];
  }
    void OnDisable()
    {
        isEnglish = true;
        UpdateDescription();
    }
}