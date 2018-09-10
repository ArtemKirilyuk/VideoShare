using System.Collections.Generic;

namespace VideoShare.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Video> Videos { get; set; }
        public Category()
        {
            Videos = new List<Video>();
        }
    }
}
