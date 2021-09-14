using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.WebModel
{
    [DataContract]
    public class CategoryModel : BaseModel
    {
        private int categoryId;
        private string name;
        private int channelCount;

        public CategoryModel()
        {

        }

        public CategoryModel(Category model)
        {
            if (model != null)
            {
                CategoryId = model.CategoryId;
                Name = model.Name;
            }
        }

        [DataMember]
        public int CategoryId
        {
            get
            {
                return this.categoryId;
            }
            set
            {
                this.categoryId = value;
            }
        }

        [DataMember]
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        [DataMember]
        public int ChannelCount
        {
            get
            {
                return this.channelCount;
            }
            set
            {
                this.channelCount = value;
            }
        }

    }
}
