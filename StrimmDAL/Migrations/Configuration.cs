namespace StrimmDAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    internal sealed class Configuration : DbMigrationsConfiguration<StrimmDAL.StrimmDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "StrimmDAL.StrimmDB";
        }
        protected override void Seed(StrimmDAL.StrimmDB context)
        {
            //context.State.AddOrUpdate(s => s.stateId,

            //  new State { state = "Other", stateId = 1, countryId = 3 });
            
        }
        //CHANNEL CATEGORIES
        //context.ChannelCategory.AddOrUpdate(c => c.channelCategoryId,
        //new ChannelCategory() { channelCategoryId = 1, category = "mixed" },
        // new ChannelCategory() { channelCategoryId = 2, category = "animals & nature" },
        //new ChannelCategory() { channelCategoryId = 3, category = "business" },
        //new ChannelCategory() { channelCategoryId = 4, category = "education" },
        //new ChannelCategory() { channelCategoryId = 5, category = "entertainment" },
        //new ChannelCategory() { channelCategoryId = 6, category = "food" },
        //new ChannelCategory() { channelCategoryId = 7, category = "games" },
        //new ChannelCategory() { channelCategoryId = 8, category = "health & sport" },
        //new ChannelCategory() { channelCategoryId = 9, category = "history" },
        //new ChannelCategory() { channelCategoryId = 10, category = "home & family" },
        //new ChannelCategory() { channelCategoryId = 11, category = "music & art" },
        //new ChannelCategory() { channelCategoryId = 12, category = "news" },
        //new ChannelCategory() { channelCategoryId = 13, category = "other" },
        //new ChannelCategory() { channelCategoryId = 14, category = "politics" },
        //new ChannelCategory() { channelCategoryId = 15, category = "science & technology" },
        //new ChannelCategory() { channelCategoryId = 16, category = "technical & industrial" },
        //new ChannelCategory() { channelCategoryId = 17, category = "travel" });
        //new ChannelCategory() { channelCategoryId = 18, category = "fashion/beauty" },
          //  new ChannelCategory() { channelCategoryId = 19, category = "comedy/humor" });
         //context.Country.AddOrUpdate(c => c.countryId,
         //       new Country() { countryId = 1, country = "USA" },
         //     new Country() { countryId = 2, country = "Canada" });
         //   context.State.AddOrUpdate(s => s.stateId,
         //      new State { state = "Alabama", stateId = 1, countryId = 1 },
         //      new State { state = "Alaska", stateId = 2, countryId = 1 },
         //      new State { state = "Arizona", stateId = 3, countryId = 1 },
         //      new State { state = "Arkansas", stateId = 4, countryId = 1 },
         //      new State { state = "California", stateId = 5, countryId = 1 },
         //      new State { state = "Colorado", stateId = 6, countryId = 1 },
         //      new State { state = "Connecticut", stateId = 7, countryId = 1 },
         //      new State { state = "Delaware", stateId = 8, countryId = 1 },
         //      new State { state = "District Of Columbia", stateId = 9, countryId = 1 },
         //      new State { state = "Florida", stateId = 10, countryId = 1 },
         //      new State { state = "Georgia", stateId = 11, countryId = 1 },
         //      new State { state = "Hawaii", stateId = 12, countryId = 1 },
         //      new State { state = "Idaho", stateId = 13, countryId = 1 },
         //      new State { state = "Illinois", stateId = 14, countryId = 1 },
         //      new State { state = "Indiana", stateId = 15, countryId = 1 },
         //      new State { state = "Iowa", stateId = 16, countryId = 1 },
         //      new State { state = "Kansas", stateId = 17, countryId = 1 },
         //      new State { state = "Kentucky", stateId = 18, countryId = 1 },
         //      new State { state = "Louisiana", stateId = 19, countryId = 1 },
         //      new State { state = "Maine", stateId = 20, countryId = 1 },
         //      new State { state = "Maryland", stateId = 21, countryId = 1 },
         //      new State { state = "Massachusetts", stateId = 22, countryId = 1 },
         //       new State { state = "Michigan", stateId = 23, countryId = 1 },
         //      new State { state = "Minnesota", stateId = 24, countryId = 1 },
         //      new State { state = "Mississippi", stateId = 25, countryId = 1 },
         //      new State { state = "Missouri", stateId = 26, countryId = 1 },
         //      new State { state = "Montana", stateId = 27, countryId = 1 },
         //      new State { state = "Nebraska", stateId = 28, countryId = 1 },
         //      new State { state = "Nevada", stateId = 29, countryId = 1 },
         //      new State { state = "New Hampshire", stateId = 30, countryId = 1 },
         //      new State { state = "New Jersey", stateId = 31, countryId = 1 },
         //       new State { state = "New Mexico", stateId = 32, countryId = 1 },
         //      new State { state = "New York", stateId = 33, countryId = 1 },
         //      new State { state = "North Carolina", stateId = 34, countryId = 1 },
         //      new State { state = "North Dakota", stateId = 35, countryId = 1 },
         //      new State { state = "Ohio", stateId = 36, countryId = 1 },
         //      new State { state = "Oklahoma", stateId = 37, countryId = 1 },
         //      new State { state = "Oregon", stateId = 38, countryId = 1 },
         //      new State { state = "Pennsylvania", stateId = 39, countryId = 1 },
         //      new State { state = "Rhode Island", stateId = 40, countryId = 1 },
         //      new State { state = "South Carolina", stateId = 41, countryId = 1 },
         //     new State { state = "South Dakota", stateId = 42, countryId = 1 },
         //      new State { state = "Tennessee", stateId = 43, countryId = 1 },
         //      new State { state = "Texas", stateId = 44, countryId = 1 },
         //      new State { state = "Utah", stateId = 45, countryId = 1 },
         //      new State { state = "Vermont", stateId = 46, countryId = 1 },
         //     new State { state = "Virginia", stateId = 47, countryId = 1 },
         //      new State { state = "Washington", stateId = 48, countryId = 1 },
         //      new State { state = "West Virginia", stateId = 49, countryId = 1 },
         //      new State { state = "Wisconsin", stateId = 50, countryId = 1 },
         //      new State { state = "Wyoming", stateId = 51, countryId = 1 },
         //      //canada states
         //     new State { state = "Ontario", stateId = 52, countryId = 2 },
         //     new State { state = "Québec", stateId = 53, countryId = 2 },
         //     new State { state = "Alberta", stateId = 54, countryId = 2 },
         //     new State { state = "Manitoba", stateId = 55, countryId = 2 },
         //     new State { state = "Saskatchewan", stateId = 56, countryId = 2 },
         //     new State { state = "Nova Scotia", stateId = 57, countryId = 2 },
         //     new State { state = "New Brunswick", stateId = 58, countryId = 2 },
         //     new State { state = "Newfoundland and Labrado", stateId = 59, countryId = 2 },
         //     new State { state = "Prince Edward Island", stateId = 60, countryId = 2 },
         //     new State { state = "Northwest", stateId = 61, countryId = 2 },
         //     new State { state = "Northwest Territories", stateId = 62, countryId = 2 },
         //     new State { state = "Yukon", stateId = 63, countryId = 2 },
         //     new State { state = "Nunavut", stateId = 64, countryId = 2 },
         //     new State { state = "British Columbia", stateId = 65, countryId = 2 }
         //      );
    }
}
