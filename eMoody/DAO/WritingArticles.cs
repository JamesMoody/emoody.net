using eMoody.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMoody.DAO
{
    public static class WritingArticles
    {

        #region article - aPoem

        public static Article createAPoem()
        {
            return new Article()
            {
                ArticleId = "aPoem",
                Title = "Anti Poem",
                Author = "James Moody",
                Content = "I wrote this back in 2009 as a college assignment. It's bad, I know... It seemed like a good idea at the time!" +
                            "\n\n" +
                            "    Rhyming is a constant fright;\n" +
                            "    like angry bears in your campsite.\n" +
                            "\n" +
                            "    Imagery is always hidden;\n" +
                            "    sailors never see the sirens.\n" +
                            "\n" +
                            "    Ragged lines so confuse me;\n" +
                            "    sounding as clear as scratched CDs.\n" +
                            "\n" +
                            "    Cultured caste is so cliché;\n" +
                            "    latte drinking, only on doomsday.\n" +
                            "\n" +
                            "    Who dreamt this hideous torture?\n" +
                            "    Poems are things I can never author!\n",
                dCreated = new DateTime()
            };
        }

        #endregion

        #region article - excitement

        public static Article createExcitement()
        {
            return new Article()
            {
                ArticleId = "excitement",
                Title = "Excitement",
                Author = "Titus Moody",
                Content = "Titus won a blue ribbon on this while in 1st grade.\n\n" +
                          "    Excitement is red\n" +
                          "    It tastes like chocolate\n" +
                          "    It smells like popcorn\n" +
                          "    And reminds me of getting a new toy\n" +
                          "    Excitement sounds like roller coasters\n" +
                          "    Excitement makes me feel like jumping high\n",
                dCreated = new DateTime()
            };
        }

        #endregion

    }
}
