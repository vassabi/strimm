using Strimm.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Repositories
{
    public class PostRepository : RepositoryBase, IPostRepository
    {
        public PostRepository()
            : base()
        {

        }

        public void InsertPost(Model.Post post)
        {
            throw new NotImplementedException();
        }

        public Model.Post GetPostByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(Model.Post post)
        {
            throw new NotImplementedException();
        }
    }
}
