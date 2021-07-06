using System;
using System.Xml;
using System.Collections.Generic;

namespace DbDevOps.BackEnd.XmlModel {
    /**
     * Depending on the manipulated node several modifications can be applied.
     * All xml elements can have their xml attributes changed (except
     * the ID attribute, which is manipulated by the DBMS). Nodes with 
     * children can also have modifications such as add/remove a child.
     * 
     * The application has to keep a copy of the entire original schema 
     * to be used in back modifications, to calculate the modifications
     * applied in order to generate the SQL script and to perform safety/healty
     * verifications before generating the migration scripts.
     * 
     * The DBModelNode class need to have a filed "modified" to indicate 
     * if some modification has been performed in a node. Obviously, any
     * modification in a child node generates a modification in its parent 
     * node. 
     */

    public interface DBModelNode {
        public DBModelNodeList<DBAttributeNode> Attributes { get; set; }
        public DBModelNode Parent { get; set; }
        public bool modified { get; set; }
    }
    /**
     * An abstract DBModelNode to be the superclass of all concrete classes.
     */
    public abstract class AbstractDBModelNode : DBModelNode {

        public DBModelNodeList<DBAttributeNode> Attributes { get; set; }
        public DBModelNode Parent { get; set; }
        public bool modified { get; set; }

        public AbstractDBModelNode() {
            this.Attributes = new DBModelNodeList<DBAttributeNode>();
        }

        /**
         * It changes the value of an attribute. If the attribute is
         * absent, then the new attribute is added is added. If the value is 
         * null, its value is filled with "".
         */
        public void setAttribute(string name, string value) {
            DBAttributeNode attr = new DBAttributeNode();
            attr.attributeName = name;
            if (value != null) { //if has some value
                attr.attributeValue = value;
            }
            Attributes.Add(attr);
        }
    }
    /**
     * DBXmlNode is the only class whose parent is null.
     */
    public class DBXmlNode : AbstractDBModelNode {
        public DBSchemaNode SchemaNode { get; set; }
    }
    public class DBSchemaNode : AbstractDBModelNode {

        public DBModelNodeList<DBModelNode> Tables { get; set; }
        public DBModelNodeList<DBModelNode> Views { get; set; }
        public DBModelNodeList<DBModelNode> Functions { get; set; }
        public DBModelNodeList<DBModelNode> Procedures { get; set; }
        public DBModelNodeList<DBModelNode> Packages { get; set; }
        public DBModelNodeList<DBModelNode> Indexes { get; set; }
        public DBModelNodeList<DBModelNode> Triggers { get; set; }
        public DBModelNodeList<DBModelNode> Sequences { get; set; }
    }

    public interface IDBModelNodeList<out T> {

    }
    public class DBModelNodeList<T> : HashSet<T>, IDBModelNodeList<T> where T : DBModelNode {

    }

    public class DBTableNode : AbstractDBModelNode {
        public DBModelNodeList<DBModelNode> Columns { get; set; }
        public DBModelNodeList<DBModelNode> Constraints { get; set; }
        public DBModelNodeList<DBModelNode> Indexes { get; set; }
    }
    public class DBViewNode : AbstractDBModelNode {
        public DBModelNodeList<DBModelNode> Columns { get; set; }
    }
    public class DBFunctionNode : AbstractDBModelNode {
        public DBModelNodeList<DBModelNode> Arguments { get; set; }
    }
    public class DBProcedureNode : AbstractDBModelNode {
        public DBModelNodeList<DBModelNode> Arguments { get; set; }
    }
    public class DBPackageNode : AbstractDBModelNode {
        public DBModelNodeList<DBModelNode> Methods { get; set; }
    }
    public class DBIndexNode : AbstractDBModelNode {

    }
    public class DBTriggerNode : AbstractDBModelNode {

    }
    public class DBSequenceNode : AbstractDBModelNode {

    }
    public class DBColumnNode : AbstractDBModelNode {

    }
    public class DBConstraintNode : AbstractDBModelNode {

    }

    public class DBArgumentNode : AbstractDBModelNode {

    }
    public class DBMethodNode : AbstractDBModelNode {
        public DBModelNodeList<DBModelNode> Arguments { get; set; }
    }
    public class DBAttributeNode : AbstractDBModelNode {
        public string attributeName {get;set;}
        public string attributeValue { get; set; }
    }
}
