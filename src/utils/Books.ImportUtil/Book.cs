namespace Books.ImportUtil
{
    public class Book
    {
        public string EAN { get; set; }
        public string TITLE { get; set; }
        public string PUB_NAME { get; set; }
        public string PUB_DATE { get; set; }
        public string AUTHOR { get; set; }
        public string PAGE_NUM { get; set; }
        public string SUBJECT_TIME { get; set; }
        public string SUBJECT { get; set; }
        public string WEIGHT { get; set; }
        public string COVER_SMALL { get; set; }
        public string COVER_MED { get; set; }

        public string COVER_LARGE { get; set; }
        public string AGE_MIN { get; set; }
        public string AGE_MAX { get; set; }
        public string ADULT_FLAG { get; set; }
        public string DEWEW_NUM { get; set; }
        public string DEPTH { get; set; }
        public string WIDTH { get; set; }

        public string LENGTH { get; set; }

        public string PRICE { get; set; }

        public string PUBLIC_DOMAIN_FLAG { get; set; }

        public string PUB_COUNTRY_CD { get; set; }

        public string US_RIGHTS_IND { get; set; }

        public string BISAC_SUBJ_CD { get; set; }
    }

    public class BookDescription
    {
        public string EAN { get; set; }
        public string DESCRIPTION { get; set; }
    }

    public class BookWithDescription
    {
        public BookWithDescription(Book book, BookDescription bookDescription)
        {
            Book = book;
            BookDescription = bookDescription;
        }

        public Book Book { get; }
        public BookDescription BookDescription { get; }
    }

}
