using System;
using System.Collections.Generic;
using System.Xml;

namespace DbDevOps.BackEnd.XmlModel {
    public class DBDevOpsModelBuilderVisitor {

        private XmlNode GetFirstChildByName(XmlNode node, String tagName) {
            XmlNode result = null;

            for (int i = 0; i < node.ChildNodes.Count; i++) {
                XmlNode currentChild = node.ChildNodes[i];
                if (currentChild.Name.Equals(tagName)) {
                    result = currentChild;
                    break;
                }
            }

            return result;
        }

        public DBModelNode Visit(XmlNode node) {

            DBModelNode result = null;
            switch (node.NodeType) {
                case XmlNodeType.Element:
                    result = VisitXmlElement((XmlElement)node);
                    break;
                default:
                    //error or visit children
                    break;
            }
            return result;
        }

        public DBModelNode VisitXmlElement(XmlElement node) {
            DBModelNode result = null;

            string tagName = ((XmlElement)node).Name;
            DBTAG currentTag = Constants.Tags.GetValueOrDefault(tagName);

            switch (currentTag) {
                case DBTAG.XML:
                    result = VisitDBXmlNode(node);
                    break;
                case DBTAG.SCHEMA:
                    result = VisitDBSchemaNode(node);
                    break;
                case DBTAG.TABLE:
                    result = VisitDBTableNode(node);
                    break;
                case DBTAG.VIEW:
                    result = VisitDBViewNode(node);
                    break;
                case DBTAG.FUNCTION:
                    result = VisitDBFunctionNode(node);
                    break;
                case DBTAG.PROCEDURE:
                    result = VisitDBProcedureNode(node);
                    break;
                case DBTAG.PACKAGE:
                    result = VisitDBPackageNode(node);
                    break;
                case DBTAG.INDEX:
                    result = VisitDBIndexNode(node);
                    break;
                case DBTAG.TRIGGER:
                    result = VisitDBTriggerNode(node);
                    break;
                case DBTAG.SEQUENCE:
                    result = VisitDBSequenceNode(node);
                    break;
                case DBTAG.COLUMN:
                    result = VisitDBColumnNode(node);
                    break;
                case DBTAG.CONSTRAINT:
                    result = VisitDBConstraintNode(node);
                    break;
                case DBTAG.ARGUMENT:
                    result = VisitDBArgumentNode(node);
                    break;
                case DBTAG.METHOD:
                    result = VisitDBMethodNode(node);
                    break;
            }

            return result;
        }

        public DBModelNode VisitDBXmlNode(XmlNode node) {
            DBXmlNode result = new DBXmlNode();

            result.SchemaNode = (DBSchemaNode)Visit(node.ChildNodes[0]);
            return result;
        }
        public DBModelNode VisitDBSchemaNode(XmlNode node) {
            DBSchemaNode result = new DBSchemaNode();


            //get schema node attributes
            foreach (XmlAttribute attribute in node.Attributes) {
                DBAttributeNode attr = new DBAttributeNode();
                attr.attributeName = attribute.Name;
                attr.attributeValue = attribute.Value;
                result.Attributes.Add(attr);
            }

            //visit all children of a schema node
            XmlNode specificChild = GetFirstChildByName(node, Constants.TABLES);
            result.Tables = BuildDBNodeList(specificChild);

            specificChild = GetFirstChildByName(node, Constants.PROCEDURES);
            result.Procedures = BuildDBNodeList(specificChild);

            specificChild = GetFirstChildByName(node, Constants.VIEWS);
            result.Views = BuildDBNodeList(specificChild);

            specificChild = GetFirstChildByName(node, Constants.FUNCTIONS);
            result.Functions = BuildDBNodeList(specificChild);

            specificChild = GetFirstChildByName(node, Constants.PACKAGES);
            result.Packages = BuildDBNodeList(specificChild);

            specificChild = GetFirstChildByName(node, Constants.SEQUENCES);
            result.Sequences = BuildDBNodeList(specificChild);

            specificChild = GetFirstChildByName(node, Constants.INDEXES);
            result.Indexes = BuildDBNodeList(specificChild);

            specificChild = GetFirstChildByName(node, Constants.TRIGGERS);
            result.Triggers = BuildDBNodeList(specificChild);

            return result;
        }
        private DBModelNodeList<DBModelNode> BuildDBNodeList(XmlNode node) {

            DBModelNodeList<DBModelNode> result = new DBModelNodeList<DBModelNode>();

            if (node != null) {
                XmlNodeList nodeList = node.ChildNodes;
                for (int i = 0; i < nodeList.Count; i++) {
                    XmlNode currentChild = nodeList[i];
                    DBModelNode currentDBModelNode = Visit(currentChild);
                    result.Add(currentDBModelNode);
                }
            }
            return result;
        }
        public DBTableNode VisitDBTableNode(XmlNode node) {
            DBTableNode result = new DBTableNode();

            //get node attributes
            foreach (XmlAttribute attribute in node.Attributes) {
                DBAttributeNode attr = new DBAttributeNode();
                attr.attributeName = attribute.Name;
                attr.attributeValue = attribute.Value;
                result.Attributes.Add(attr);
            }

            //tables are composed by columns, constraints and indexes
            XmlNode specificChild = GetFirstChildByName(node, Constants.COLUMNS);
            result.Columns = BuildDBNodeList(specificChild);

            specificChild = GetFirstChildByName(node, Constants.CONSTRAINTS);
            result.Constraints = BuildDBNodeList(specificChild);

            specificChild = GetFirstChildByName(node, Constants.INDEXES);
            result.Indexes = BuildDBNodeList(specificChild);

            return result;
        }
        public DBProcedureNode VisitDBProcedureNode(XmlNode node) {
            DBProcedureNode result = new DBProcedureNode();

            //get node attributes
            foreach (XmlAttribute attribute in node.Attributes) {
                DBAttributeNode attr = new DBAttributeNode();
                attr.attributeName = attribute.Name;
                attr.attributeValue = attribute.Value;
                result.Attributes.Add(attr);
            }

            //procedures are composed by arguments
            XmlNode specificChild = GetFirstChildByName(node, Constants.ARGUMENTS);
            result.Arguments = BuildDBNodeList(specificChild);

            return result;
        }

        public DBFunctionNode VisitDBFunctionNode(XmlNode node) {
            DBFunctionNode result = new DBFunctionNode();

            //get node attributes
            foreach (XmlAttribute attribute in node.Attributes) {
                DBAttributeNode attr = new DBAttributeNode();
                attr.attributeName = attribute.Name;
                attr.attributeValue = attribute.Value;
                result.Attributes.Add(attr);
            }

            //functions are composed by arguments
            XmlNode specificChild = GetFirstChildByName(node, Constants.ARGUMENTS);
            result.Arguments = BuildDBNodeList(specificChild);

            return result;
        }

        public DBPackageNode VisitDBPackageNode(XmlNode node) {
            DBPackageNode result = new DBPackageNode();

            //get node attributes
            foreach (XmlAttribute attribute in node.Attributes) {
                DBAttributeNode attr = new DBAttributeNode();
                attr.attributeName = attribute.Name;
                attr.attributeValue = attribute.Value;
                result.Attributes.Add(attr);
            }

            //packages are composed by methods
            XmlNode specificChild = GetFirstChildByName(node, Constants.METHODS);
            result.Methods = BuildDBNodeList(specificChild);

            return result;
        }

        public DBIndexNode VisitDBIndexNode(XmlNode node) {
            DBIndexNode result = new DBIndexNode();

            //get node attributes
            foreach (XmlAttribute attribute in node.Attributes) {
                DBAttributeNode attr = new DBAttributeNode();
                attr.attributeName = attribute.Name;
                attr.attributeValue = attribute.Value;
                result.Attributes.Add(attr);
            }

            return result;
        }

        public DBTriggerNode VisitDBTriggerNode(XmlNode node) {
            DBTriggerNode result = new DBTriggerNode();

            //get node attributes
            foreach (XmlAttribute attribute in node.Attributes) {
                DBAttributeNode attr = new DBAttributeNode();
                attr.attributeName = attribute.Name;
                attr.attributeValue = attribute.Value;
                result.Attributes.Add(attr);
            }

            return result;
        }

        public DBViewNode VisitDBViewNode(XmlNode node) {
            DBViewNode result = new DBViewNode();

            //get node attributes
            foreach (XmlAttribute attribute in node.Attributes) {
                DBAttributeNode attr = new DBAttributeNode();
                attr.attributeName = attribute.Name;
                attr.attributeValue = attribute.Value;
                result.Attributes.Add(attr);
            }

            //views are composed by columns
            XmlNode specificChild = GetFirstChildByName(node, Constants.COLUMNS);
            result.Columns = BuildDBNodeList(specificChild);

            return result;
        }

        public DBSequenceNode VisitDBSequenceNode(XmlNode node) {
            DBSequenceNode result = new DBSequenceNode();

            //get node attributes
            foreach (XmlAttribute attribute in node.Attributes) {
                DBAttributeNode attr = new DBAttributeNode();
                attr.attributeName = attribute.Name;
                attr.attributeValue = attribute.Value;
                result.Attributes.Add(attr);
            }

            return result;
        }

        public DBColumnNode VisitDBColumnNode(XmlNode node) {
            DBColumnNode result = new DBColumnNode();

            //get node attributes
            foreach (XmlAttribute attribute in node.Attributes) {
                DBAttributeNode attr = new DBAttributeNode();
                attr.attributeName = attribute.Name;
                attr.attributeValue = attribute.Value;
                result.Attributes.Add(attr);
            }

            return result;
        }

        public DBConstraintNode VisitDBConstraintNode(XmlNode node) {
            DBConstraintNode result = new DBConstraintNode();

            //get node attributes
            foreach (XmlAttribute attribute in node.Attributes) {
                DBAttributeNode attr = new DBAttributeNode();
                attr.attributeName = attribute.Name;
                attr.attributeValue = attribute.Value;
                result.Attributes.Add(attr);
            }

            return result;
        }

        public DBMethodNode VisitDBMethodNode(XmlNode node) {
            DBMethodNode result = new DBMethodNode();

            //get node attributes
            foreach (XmlAttribute attribute in node.Attributes) {
                DBAttributeNode attr = new DBAttributeNode();
                attr.attributeName = attribute.Name;
                attr.attributeValue = attribute.Value;
                result.Attributes.Add(attr);
            }

            //methods are composed by arguments
            XmlNode specificChild = GetFirstChildByName(node, Constants.ARGUMENTS);
            result.Arguments = BuildDBNodeList(specificChild);

            return result;
        }

        public DBArgumentNode VisitDBArgumentNode(XmlNode node) {
            DBArgumentNode result = new DBArgumentNode();

            //get node attributes
            foreach (XmlAttribute attribute in node.Attributes) {
                DBAttributeNode attr = new DBAttributeNode();
                attr.attributeName = attribute.Name;
                attr.attributeValue = attribute.Value;
                result.Attributes.Add(attr);
            }

            return result;
        }

    }
}
