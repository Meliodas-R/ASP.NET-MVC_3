using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;


namespace Dal
{
    [TestClass]
    public class DaoEntityProductoTest
    {
        private static readonly IDaoProducto dao = new DaoEntityProducto();

        private DbConnection ObtenerConexion()
        {
            return new SqlConnection(System.Configuration.ConfigurationManager.
                ConnectionStrings["TiendaMVCContext"].ConnectionString);
        }

        [TestInitialize]
        public void PreTest()
        {
            using (DbConnection con = ObtenerConexion())
            {
                con.Open();
                DbCommand com = con.CreateCommand();

                com.CommandText = "TRUNCATE Table Productos";

                com.CommandText = "INSERT INTO Productos (Nombre, Precio) VALUES ('Tomate', 0.85)";
                com.ExecuteNonQuery();

                com.CommandText = "INSERT INTO Productos (Nombre, Precio) VALUES ('Lechuga', 1.00)";
                com.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void PostTest()
        {
            using (DbConnection con = ObtenerConexion())
            {
                con.Open();

                DbCommand com = con.CreateCommand();
                com.CommandText = "TRUNCATE Table Productos";

                com.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void ObtenerTodos()
        {
            List<Producto> productos = (List<Producto>)dao.ObtenerTodos() as List<Producto>;

            // Comprueba que la lista de productos no sea nula.
            Assert.IsNotNull(productos);

            // Comprueba que haya dos productos en la tabla.
            Assert.AreEqual(2, productos.Count);

            // Comprueba que los productos entregados concuerdan con los registros de la BD.
            Assert.AreEqual(new Producto() { Id = 1L, Nombre = "Tomate", Precio = 0.85m }, productos[0]);
            Assert.AreEqual(new Producto() { Id = 2L, Nombre = "Lechuga", Precio = 1.00m }, productos[1]);
        }

        [TestMethod]
        public void ObtenerPorId()
        {
            Producto producto = dao.ObtenerPorId(1L);

            // Comprueba que el producto buscado no sea nulo.
            Assert.IsNotNull(producto);

            // Comprueba que el producto entregado concuerda con el registro de la BD.
            Assert.AreEqual(new Producto() { Id = 1L, Nombre = "Tomate", Precio = 0.85m }, producto);
        }

        [TestMethod]
        public void Insertar()
        {
            Producto nuevo = new Producto() { Nombre = "Azúcar", Precio = 0.97m };
            dao.Insertar(nuevo);

            Assert.AreEqual(3L, nuevo.Id);

            using (DbConnection con = ObtenerConexion())
            {
                con.Open();

                DbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Productos WHERE Id = 3";

                DbDataReader dr = com.ExecuteReader();

                Assert.IsTrue(dr.Read());

                Assert.AreEqual("Azúcar", dr["Nombre"]);
                Assert.AreEqual(0.97m, dr["Precio"]);
            }
        }

        [TestMethod]
        public void Modificar()
        {
            Producto modificado = new Producto() { Id = 2L, Nombre = "Modificar", Precio = 0.97m };
            dao.Modificar(modificado);

            Assert.AreEqual(2L, modificado.Id);

            using (DbConnection con = ObtenerConexion())
            {
                con.Open();

                DbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Productos WHERE Id = 2";

                DbDataReader dr = com.ExecuteReader();

                Assert.IsTrue(dr.Read());

                Assert.AreEqual("Modificar", dr["Nombre"]);
                Assert.AreEqual(0.97m, dr["Precio"]);
            }
        }

        [TestMethod]
        public void Borrar()
        {
            dao.Borrar(1L);

            using (DbConnection con = ObtenerConexion())
            {
                con.Open();

                DbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Productos WHERE Id = 1";

                DbDataReader dr = com.ExecuteReader();

                Assert.IsFalse(dr.Read());
            }

        }
    }
}
