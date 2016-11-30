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
        public string Genre { get; set; }
        //TODO: MOAR INFO
    }
}
