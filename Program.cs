using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AnotherTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = "Untitled.x3d";
            string outputFileName = "Filtered.x3d";

            // Configurar las opciones para evitar que se cargue la DTD
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Ignore;

            // Crear un lector para el archivo X3D
            using (XmlReader reader = XmlReader.Create(fileName, settings))
            {
                // Crear un escritor para el archivo de salida
                using (XmlTextWriter writer = new XmlTextWriter(outputFileName, null))
                {
                    // Configurar el escritor para que use indentación
                    writer.Formatting = Formatting.Indented;

                    // Escribir la declaración XML
                    writer.WriteStartDocument();

                    // Escribir la etiqueta raíz X3D
                    writer.WriteStartElement("X3D");
                    //writer.WriteAttributeString("xmlns", "http://www.web3d.org/specifications/x3d-namespace");
                    writer.WriteAttributeString("version", "3.3");

                    // Escribir la etiqueta Scene
                    writer.WriteStartElement("Scene");

                    // Leer el archivo X3D y filtrar las etiquetas
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            switch (reader.Name)
                            {
                                case "X3D":
                                    // Escribir la etiqueta X3D y sus atributos
                                    writer.WriteStartElement("X3D");
                                    writer.WriteAttributes(reader, true);
                                    break;

                                case "Scene":
                                    // Escribir la etiqueta Scene
                                    writer.WriteStartElement("Scene");
                                    break;

                                case "ProtoInstance":
                                    // Escribir la etiqueta ProtoInstance y sus atributos
                                    writer.WriteStartElement("ProtoInstance");
                                    writer.WriteAttributes(reader, true);
                                    break;

                                case "Transform":
                                    // Escribir la etiqueta Transform y sus atributos
                                    writer.WriteStartElement("Transform");
                                    writer.WriteAttributes(reader, true);
                                    break;

                                case "Shape":
                                    // Escribir la etiqueta Shape y sus atributos
                                    writer.WriteStartElement("Shape");
                                    writer.WriteAttributes(reader, true);
                                    break;

                                case "fieldValue":
                                    // Escribir la etiqueta fieldValue y sus atributos
                                    writer.WriteStartElement("fieldValue");
                                    writer.WriteAttributes(reader, true);
                                    writer.WriteEndElement();
                                    break;

                                case "Appearance":
                                    // Escribir la etiqueta Appearance y sus atributos
                                    writer.WriteStartElement("Appearance");
                                    writer.WriteAttributes(reader, true);
                                    break;

                                case "Material":
                                    // Escribir la etiqueta Material y sus atributos
                                    writer.WriteStartElement("Material");
                                    writer.WriteAttributes(reader, true);
                                    writer.WriteEndElement(); // Material
                                    writer.WriteEndElement(); // Appearance
                                    break;

                                case "IndexedFaceSet":
                                    // Escribir la etiqueta IndexedFaceSet y sus atributos
                                    writer.WriteStartElement("IndexedFaceSet");
                                    writer.WriteAttributes(reader, true);
                                    writer.WriteEndElement(); // IndexedFaceSet
                                    writer.WriteEndElement(); // Shape
                                    break;

                                default:
                                    // Saltar cualquier otra etiqueta
                                    reader.Skip();
                                    break;
                            }
                        }
                    }

                    // Escribir el final del archivo X3D
                    writer.WriteEndElement(); // Scene
                    writer.WriteEndElement(); // X3D
                    writer.WriteEndDocument();

                    // Cerrar los objetos de lectura y escritura
                    reader.Close();
                    writer.Close();
                }
            }
        }
    }
}
