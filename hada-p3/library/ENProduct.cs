using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace library
{
    public class ENProduct
    {
        // añadimos _ delante del nombre del atributo privado para diferencias de los pasados por
        // parametro en constructor de copia
        private string _code; 
        private string _name;
        private int _amount; 
        private float _price;
        private int _category;
        private DateTime _creationDate;

        // Propiedades públicas para acceder a los campos privados
        public string Code { get => _code; set => _code = value; }
        public string Name { get => _name; set => _name = value; }
        public int Amount { get => _amount; set => _amount = value; }
        public float Price { get => _price; set => _price = value; }
        public int Category { get => _category; set => _category = value; }
        public DateTime CreationDate { get => _creationDate; set => _creationDate = value; }

        public ENProduct()
        {

        }

        public ENProduct(string code, string name, int amount, float price, int category, DateTime creationDate)
        {
            _code = code;
            _name = name;
            _amount = amount;
            _price = price;
            _category = category;
            _creationDate = creationDate; 
        }

        public bool Create()
        {
            CADProduct product = new CADProduct();
            return product.Create(this);
        }

        public bool Update()
        {
            CADProduct product = new CADProduct();
            return product.Update(this);
        }

        public bool Delete()
        {
            CADProduct product = new CADProduct();
            return product.Delete(this);
        }

        public bool Read()
        {
            CADProduct product = new CADProduct();
            return product.Read(this);
        }

        public bool ReadFirst()
        {
            CADProduct product = new CADProduct();
            return product.ReadFirst(this); 
        }

        public bool ReadNext()
        {
            CADProduct product = new CADProduct();
            return product.ReadNext(this);
        }

        public bool ReadPrev()
        {
            CADProduct product = new CADProduct();
            return product.ReadPrev(this);
        }
    }
}
