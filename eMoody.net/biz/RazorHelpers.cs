using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMoody.net.biz
{
    public static class RazorHelpers
    {

        #region helpers - bindToInt

        public static int bindToInt(object value, int defaultValue = -1) {
            if (value is string strValue) {               // convert to a string & parse
                int retVal;
                if (int.TryParse(strValue, out retVal)) { // does this string contain number?
                    return retVal;                        // yerp
                } else {
                    return defaultValue;                  // nope, return the default
                }

            } else if (value is int intVal) {             // shouldn't be possible, but just in case...
                return intVal;                            // value is an in hidden behind the object type; convert and return. Note: up/down casting is possible here. 

            } else {
                return defaultValue;
            }
        }

        #endregion

    }
}
