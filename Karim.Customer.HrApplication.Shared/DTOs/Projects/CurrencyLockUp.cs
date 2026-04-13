using System.ComponentModel.DataAnnotations;

namespace Karim.Customer.HrApplication.Shared.DTOs.Projects
{
    public enum CurrencyLockUp
    {
        // A
        [Display(Name = "UAE Dirham")]
        AED = 784,
        [Display(Name = "Afghan Afghani")]
        AFN = 971,
        [Display(Name = "Albanian Lek")]
        ALL = 008,
        [Display(Name = "Armenian Dram")]
        AMD = 051,
        [Display(Name = "Netherlands Antillean Guilder")]
        ANG = 532,
        [Display(Name = "Angolan Kwanza")]
        AOA = 973,
        [Display(Name = "Argentine Peso")]
        ARS = 032,
        [Display(Name = "Australian Dollar")]
        AUD = 036,
        [Display(Name = "Aruban Florin")]
        AWG = 533,
        [Display(Name = "Azerbaijani Manat")]
        AZN = 944,

        // B
        [Display(Name = "Bosnia-Herzegovina Convertible Mark")]
        BAM = 977,
        [Display(Name = "Barbadian Dollar")]
        BBD = 052,
        [Display(Name = "Bangladeshi Taka")]
        BDT = 050,
        [Display(Name = "Bulgarian Lev")]
        BGN = 975,
        [Display(Name = "Bahraini Dinar")]
        BHD = 048,
        [Display(Name = "Burundian Franc")]
        BIF = 108,
        [Display(Name = "Bermudian Dollar")]
        BMD = 060,
        [Display(Name = "Brunei Dollar")]
        BND = 096,
        [Display(Name = "Bolivian Boliviano")]
        BOB = 068,
        [Display(Name = "Brazilian Real")]
        BRL = 986,
        [Display(Name = "Bahamian Dollar")]
        BSD = 044,
        [Display(Name = "Bhutanese Ngultrum")]
        BTN = 064,
        [Display(Name = "Botswana Pula")]
        BWP = 072,
        [Display(Name = "Belarusian Ruble")]
        BYN = 933,
        [Display(Name = "Belize Dollar")]
        BZD = 084,

        // C
        [Display(Name = "Canadian Dollar")]
        CAD = 124,
        [Display(Name = "Congolese Franc")]
        CDF = 976,
        [Display(Name = "Swiss Franc")]
        CHF = 756,
        [Display(Name = "Chilean Peso")]
        CLP = 152,
        [Display(Name = "Chinese Yuan")]
        CNY = 156,
        [Display(Name = "Colombian Peso")]
        COP = 170,
        [Display(Name = "Costa Rican Colón")]
        CRC = 188,
        [Display(Name = "Cuban Peso")]
        CUP = 192,
        [Display(Name = "Cape Verdean Escudo")]
        CVE = 132,
        [Display(Name = "Czech Koruna")]
        CZK = 203,

        // D
        [Display(Name = "Djiboutian Franc")]
        DJF = 262,
        [Display(Name = "Danish Krone")]
        DKK = 208,
        [Display(Name = "Dominican Peso")]
        DOP = 214,
        [Display(Name = "Algerian Dinar")]
        DZD = 012,

        // E
        [Display(Name = "Egyptian Pound")]
        EGP = 818,
        [Display(Name = "Eritrean Nakfa")]
        ERN = 232,
        [Display(Name = "Ethiopian Birr")]
        ETB = 230,
        [Display(Name = "Euro")]
        EUR = 978,

        // F
        [Display(Name = "Fijian Dollar")]
        FJD = 242,
        [Display(Name = "Falkland Islands Pound")]
        FKP = 238,

        // G
        [Display(Name = "British Pound Sterling")]
        GBP = 826,
        [Display(Name = "Georgian Lari")]
        GEL = 981,
        [Display(Name = "Ghanaian Cedi")]
        GHS = 936,
        [Display(Name = "Gibraltar Pound")]
        GIP = 292,
        [Display(Name = "Gambian Dalasi")]
        GMD = 270,
        [Display(Name = "Guinean Franc")]
        GNF = 324,
        [Display(Name = "Guatemalan Quetzal")]
        GTQ = 320,
        [Display(Name = "Guyanese Dollar")]
        GYD = 328,

        // H
        [Display(Name = "Hong Kong Dollar")]
        HKD = 344,
        [Display(Name = "Honduran Lempira")]
        HNL = 340,
        [Display(Name = "Croatian Kuna")]
        HRK = 191,
        [Display(Name = "Haitian Gourde")]
        HTG = 332,
        [Display(Name = "Hungarian Forint")]
        HUF = 348,

        // I
        [Display(Name = "Indonesian Rupiah")]
        IDR = 360,
        [Display(Name = "Israeli New Shekel")]
        ILS = 376,
        [Display(Name = "Indian Rupee")]
        INR = 356,
        [Display(Name = "Iraqi Dinar")]
        IQD = 368,
        [Display(Name = "Iranian Rial")]
        IRR = 364,
        [Display(Name = "Icelandic Króna")]
        ISK = 352,

        // J
        [Display(Name = "Jamaican Dollar")]
        JMD = 388,
        [Display(Name = "Jordanian Dinar")]
        JOD = 400,
        [Display(Name = "Japanese Yen")]
        JPY = 392,

        // K
        [Display(Name = "Kenyan Shilling")]
        KES = 404,
        [Display(Name = "Kyrgyzstani Som")]
        KGS = 417,
        [Display(Name = "Cambodian Riel")]
        KHR = 116,
        [Display(Name = "Comorian Franc")]
        KMF = 174,
        [Display(Name = "North Korean Won")]
        KPW = 408,
        [Display(Name = "South Korean Won")]
        KRW = 410,
        [Display(Name = "Kuwaiti Dinar")]
        KWD = 414,
        [Display(Name = "Cayman Islands Dollar")]
        KYD = 136,
        [Display(Name = "Kazakhstani Tenge")]
        KZT = 398,

        // L
        [Display(Name = "Lao Kip")]
        LAK = 418,
        [Display(Name = "Lebanese Pound")]
        LBP = 422,
        [Display(Name = "Sri Lankan Rupee")]
        LKR = 144,
        [Display(Name = "Liberian Dollar")]
        LRD = 430,
        [Display(Name = "Lesotho Loti")]
        LSL = 426,
        [Display(Name = "Libyan Dinar")]
        LYD = 434,

        // M
        [Display(Name = "Moroccan Dirham")]
        MAD = 504,
        [Display(Name = "Moldovan Leu")]
        MDL = 498,
        [Display(Name = "Malagasy Ariary")]
        MGA = 969,
        [Display(Name = "Macedonian Denar")]
        MKD = 807,
        [Display(Name = "Myanmar Kyat")]
        MMK = 104,
        [Display(Name = "Mongolian Tögrög")]
        MNT = 496,
        [Display(Name = "Macanese Pataca")]
        MOP = 446,
        [Display(Name = "Mauritanian Ouguiya")]
        MRU = 929,
        [Display(Name = "Mauritian Rupee")]
        MUR = 480,
        [Display(Name = "Maldivian Rufiyaa")]
        MVR = 462,
        [Display(Name = "Malawian Kwacha")]
        MWK = 454,
        [Display(Name = "Mexican Peso")]
        MXN = 484,
        [Display(Name = "Malaysian Ringgit")]
        MYR = 458,
        [Display(Name = "Mozambican Metical")]
        MZN = 943,

        // N
        [Display(Name = "Namibian Dollar")]
        NAD = 516,
        [Display(Name = "Nigerian Naira")]
        NGN = 566,
        [Display(Name = "Nicaraguan Córdoba")]
        NIO = 558,
        [Display(Name = "Norwegian Krone")]
        NOK = 578,
        [Display(Name = "Nepalese Rupee")]
        NPR = 524,
        [Display(Name = "New Zealand Dollar")]
        NZD = 554,

        // O
        [Display(Name = "Omani Rial")]
        OMR = 512,

        // P
        [Display(Name = "Panamanian Balboa")]
        PAB = 590,
        [Display(Name = "Peruvian Sol")]
        PEN = 604,
        [Display(Name = "Papua New Guinean Kina")]
        PGK = 598,
        [Display(Name = "Philippine Peso")]
        PHP = 608,
        [Display(Name = "Pakistani Rupee")]
        PKR = 586,
        [Display(Name = "Polish Złoty")]
        PLN = 985,
        [Display(Name = "Paraguayan Guaraní")]
        PYG = 600,

        // Q
        [Display(Name = "Qatari Riyal")]
        QAR = 634,

        // R
        [Display(Name = "Romanian Leu")]
        RON = 946,
        [Display(Name = "Serbian Dinar")]
        RSD = 941,
        [Display(Name = "Russian Ruble")]
        RUB = 643,
        [Display(Name = "Rwandan Franc")]
        RWF = 646,

        // S
        [Display(Name = "Saudi Riyal")]
        SAR = 682,
        [Display(Name = "Solomon Islands Dollar")]
        SBD = 090,
        [Display(Name = "Seychellois Rupee")]
        SCR = 690,
        [Display(Name = "Sudanese Pound")]
        SDG = 938,
        [Display(Name = "Swedish Krona")]
        SEK = 752,
        [Display(Name = "Singapore Dollar")]
        SGD = 702,
        [Display(Name = "Saint Helena Pound")]
        SHP = 654,
        [Display(Name = "Sierra Leonean Leone")]
        SLL = 694,
        [Display(Name = "Somali Shilling")]
        SOS = 706,
        [Display(Name = "Surinamese Dollar")]
        SRD = 968,
        [Display(Name = "South Sudanese Pound")]
        SSP = 728,
        [Display(Name = "São Tomé and Príncipe Dobra")]
        STN = 930,
        [Display(Name = "Salvadoran Colón")]
        SVC = 222,
        [Display(Name = "Syrian Pound")]
        SYP = 760,
        [Display(Name = "Swazi Lilangeni")]
        SZL = 748,

        // T
        [Display(Name = "Thai Baht")]
        THB = 764,
        [Display(Name = "Tajikistani Somoni")]
        TJS = 972,
        [Display(Name = "Turkmenistani Manat")]
        TMT = 934,
        [Display(Name = "Tunisian Dinar")]
        TND = 788,
        [Display(Name = "Tongan Paʻanga")]
        TOP = 776,
        [Display(Name = "Turkish Lira")]
        TRY = 949,
        [Display(Name = "Trinidad and Tobago Dollar")]
        TTD = 780,
        [Display(Name = "New Taiwan Dollar")]
        TWD = 901,
        [Display(Name = "Tanzanian Shilling")]
        TZS = 834,

        // U
        [Display(Name = "Ukrainian Hryvnia")]
        UAH = 980,
        [Display(Name = "Ugandan Shilling")]
        UGX = 800,
        [Display(Name = "US Dollar")]
        USD = 840,
        [Display(Name = "Uruguayan Peso")]
        UYU = 858,
        [Display(Name = "Uzbekistani Som")]
        UZS = 860,

        // V
        [Display(Name = "Venezuelan Bolívar")]
        VES = 928,
        [Display(Name = "Vietnamese Đồng")]
        VND = 704,
        [Display(Name = "Vanuatu Vatu")]
        VUV = 548,

        // W
        [Display(Name = "Samoan Tālā")]
        WST = 882,

        // X
        [Display(Name = "Central African CFA Franc")]
        XAF = 950,
        [Display(Name = "East Caribbean Dollar")]
        XCD = 951,
        [Display(Name = "West African CFA Franc")]
        XOF = 952,
        [Display(Name = "CFP Franc")]
        XPF = 953,

        // Y
        [Display(Name = "Yemeni Rial")]
        YER = 886,

        // Z
        [Display(Name = "South African Rand")]
        ZAR = 710,
        [Display(Name = "Zambian Kwacha")]
        ZMW = 967,
        [Display(Name = "Zimbabwean Dollar")]
        ZWL = 932
    }
}
