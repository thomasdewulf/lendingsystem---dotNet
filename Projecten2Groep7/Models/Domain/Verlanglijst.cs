using System;
using System.Collections.Generic;
using System.Linq;

namespace Projecten2Groep7.Models.Domain
{
    public class Verlanglijst

    {
        public Verlanglijst()
        {
            Producten = new List<Product>();
        }

        public int VerlanglijstId { get; set; }

        #region Fields

        #endregion

        #region Properties

        public virtual ICollection<Product> Producten { get; set; }

        public int AantalItems => Producten.Count();

        #endregion

        #region Methods

        public void VoegProductToe(Product product)
        {
            Product nieuwProduct = Producten.SingleOrDefault(l => l.Equals(product));
            if (nieuwProduct == null)
            {
                Producten.Add(product);
            }
            //else
            //Product is al in de verlanglijst aanwezig
            //Via controller: TempData  melden dat het product al aanwezig is in de verlanglijst
        }

        public void MaakVerlanglijstLeeg()
        {
            Producten.Clear();
        }

        //public void HoeveelheidAfnemen(int productId)
        //{
        //    VerlanglijstLijn lijn = GeefVerlanglijstLijn(productId);
        //    if (lijn != null)
        //        lijn.Hoeveelheid--;
        //    if (lijn.Hoeveelheid <= 0)
        //        lijnen.Remove(lijn);
        //}

        public Product GeefProduct(int productId)
        {
            return Producten.SingleOrDefault(l => l.ProductId == productId);
        }

        //public void HoeveelheidToenemen(int productId)
        //{
        //    VerlanglijstLijn lijn = GeefVerlanglijstLijn(productId);
        //    if (lijn != null)
        //        lijn.Hoeveelheid++;
        //}

        public void VerwijderProduct(Product product)
        {
            Product p = GeefProduct(product.ProductId);
            if (p != null)
                Producten.Remove(product);
        }

        #endregion

        public bool BevatProduct(Product product)
        {
            return Producten.Contains(product);
        }
    }
}