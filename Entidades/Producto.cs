using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Table("Productos")]
    public class Producto : IEquatable<Producto>
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Producto);
        }

        public bool Equals(Producto other)
        {
            return other != null &&
                   Id == other.Id &&
                   Nombre == other.Nombre &&
                   Precio == other.Precio;
        }

        public override int GetHashCode()
        {
            int hashCode = -675047433;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nombre);
            hashCode = hashCode * -1521134295 + Precio.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Producto left, Producto right)
        {
            return EqualityComparer<Producto>.Default.Equals(left, right);
        }

        public static bool operator !=(Producto left, Producto right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"{Id}, {Nombre}, {Precio}";
        }
    }
}
