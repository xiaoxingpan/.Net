using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Xml.Linq;

namespace MidtermPizzaOrders
{
    public class PizzaOrder
    {
        public PizzaOrder() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        private string _clientName;

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string ClientName
        {
            get
            {
                return _clientName;
            }
            set
            {
                if (!IsClientNameValid(value, out string msg))
                { throw new ArgumentException(msg); }
                _clientName = value;
            }
        }

        private string _clientPostalCode;

        [Required]
        public string ClientPostalCode
        {
            get
            {
                return _clientPostalCode;
            }
            set
            {
                if (!IsClientPostalCodeValid(value, out string msg))
                { throw new ArgumentException(msg); }
                _clientPostalCode = value;
            }
        }


        private DateTime _deliveryDeadline;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DeliveryDeadline
        {
            get => _deliveryDeadline;
            set
            {      
                _deliveryDeadline = value;
            }
        }

        [Required]
        [EnumDataType(typeof(SizeEnum))]
        public SizeEnum Size { get; set; }

        private int _bakingTimeMinutes;
        public int BakingTimeMinutes
        {
            get
            {
                return _bakingTimeMinutes;
            }
            set
            {
                if (value < 12 || value > 18)
                {
                    throw new ArgumentException();
                }
                _bakingTimeMinutes =value;
            }
        }

        [Required]
        [EnumDataType(typeof(OrderStatusEnum))]
        public OrderStatusEnum OrderStatus { get; set; }


        public string Extras { get; set; }

        public enum SizeEnum
        {
            Small = 3,
            Medium = 7,
            Large = 12
        }

        public enum OrderStatusEnum
        {
            Placed = 0,
            Fulfilled = 1
        }

        public PizzaOrder(int id, string clientName, string clientPostalCode, DateTime deliveryDeadline, SizeEnum size, int bakingTimeMinutes, string extras, OrderStatusEnum orderStatus) 
        {
            Id = id;
            ClientName = clientName;
            ClientPostalCode = clientPostalCode;
            DeliveryDeadline = deliveryDeadline;
            Size = size;
            BakingTimeMinutes = bakingTimeMinutes;
            Extras = extras;
            OrderStatus = orderStatus;
        }

        // validation
        public static bool IsClientNameValid(string clientName, out string error)
        {
            if (clientName.Length < 1 || clientName.Length > 100 || !Regex.IsMatch(clientName, "^[A-Za-z]+$"))
            {
                error = "Name must be 1-100 characters long, only letters";
                return false;
            }
            error = null;
            return true; 
        }

        public static bool IsClientPostalCodeValid(string clientPostalCode, out string error)
        {
            string postalCodePattern = @"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$";
            if (!Regex.IsMatch(clientPostalCode, postalCodePattern))
            {
                error = "Postal code must be in the format 'A1A 1A1'";
                return false;
            }
            error = null;
            return true;
        }
        public override string ToString()
        {
            return $"{ClientName}, {ClientPostalCode}, {DeliveryDeadline}, {Size}, {BakingTimeMinutes}, {Extras}, {OrderStatus}";
        }
    }
}
