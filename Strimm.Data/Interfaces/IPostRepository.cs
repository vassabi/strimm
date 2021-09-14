using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IPostRepository
    {
        void InsertPost(Post post);

        Post GetPostByUserId(int userId);

        void UpdatePost(Post post);
    }
}
