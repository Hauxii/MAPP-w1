namespace MovieSearch.Model
{
    public class Movie
    {
        public Movie() { }
        public Movie(string title /*TODO: AND MOAR*/)
        {
            this.Title = title;
        }
        public string Title { get; set; }
		public string Year { get; set; }
        public string Genre { get; set; }
		public string Overview { get; set; }
		public string Poster { get; set; }
		public string Cast { get; set; }
        //TODO: MOAR INFO
    }
}
