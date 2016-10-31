using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projecten2Groep7.ViewModels
{
    public class ViewModelFactory
    {
        public ViewModel CreateViewModel(string type)
        {
            switch (type.ToLower())
            {
                case"product": return new ProductViewModel();
                case "verlanglijstDetail": return new VerlanglijstDetailViewModel();
                case "verlanglijst":return new VerlanglijstViewModel();
                default:
                    return null;
            }
        }
    }
}