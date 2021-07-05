using System;
using System.Collections.Generic;
using System.Xml;

namespace DbDevOps.BackEnd.XmlModel {

    public enum DBTAG {
        XML, SCHEMA, TABLES, VIEWS, FUNCTIONS, PROCEDURES, PACKAGES,
        TRIGGERS, SEQUENCES, TABLE, VIEW, FUNCTION, PROCEDURE,
        PACKAGE, INDEX, TRIGGER, SEQUENCE, COLUMNS, COLUMN, CONSTRAINTS,
        CONSTRAINT, ARGUMENTS, METHODS, ARGUMENT, METHOD
    }
    public class Constants {

        //Most high-level TAG name (level 0)
        public static string XML = "XML";

        //TAG names (level 1) define elements contained in level-0 element
        public static string SCHEMA = "SCHEMA";

        //TAG names (level 2) define elements contained in level-1 element
        public static string TABLES = "TABLES";
        public static string VIEWS = "VIEWS";
        public static string FUNCTIONS = "FUNCTIONS";
        public static string PROCEDURES = "PROCEDURES";
        public static string PACKAGES = "PACKAGES";
        public static string INDEXES = "INDEXES";
        public static string TRIGGERS = "TRIGGERS";
        public static string SEQUENCES = "SEQUENCES";

        //TAG names (level 3) define elements contained in level-2 tags
        public static string TABLE = "TABLE";
        public static string VIEW = "VIEW";
        public static string FUNCTION = "FUNCTION";
        public static string PROCEDURE = "PROCEDURE";
        public static string PACKAGE = "PACKAGE";
        public static string INDEX = "INDEX";
        public static string TRIGGER = "TRIGGER";
        public static string SEQUENCE = "SEQUENCE";

        //TAG names (level 4) define elements contained in level-3 tags
        public static string COLUMNS = "COLUMNS";
        public static string CONSTRAINTS = "CONSTRAINTS";
        public static string ARGUMENTS = "ARGUMENTS";
        public static string METHODS = "METHODS";

        //TAG names (level 5) define elements contained in level-4 tags
        public static string COLUMN = "COLUMN";
        public static string CONSTRAINT = "CONSTRAINT";
        public static string ARGUMENT = "ARGUMENT";
        public static string METHOD = "METHOD";

        //the NAME attribute of allmost nodes of a scehma
        public static string ATTRIBUTE_NAME = "NAME";




        public static Dictionary<string, DBTAG> Tags { get; }
            = new Dictionary<string, DBTAG>() {
                { XML,DBTAG.XML },
                { SCHEMA,DBTAG.SCHEMA },
                { TABLE,DBTAG.TABLE },
                { VIEW,DBTAG.VIEW },
                { FUNCTION,DBTAG.FUNCTION },
                { PROCEDURE,DBTAG.PROCEDURE },
                { PACKAGE,DBTAG.PACKAGE },
                { INDEX,DBTAG.INDEX },
                { TRIGGER,DBTAG.TRIGGER },
                { SEQUENCE,DBTAG.SEQUENCE },
                { COLUMN,DBTAG.COLUMN },
                { CONSTRAINT,DBTAG.CONSTRAINT },
                { ARGUMENT,DBTAG.ARGUMENT },
                { METHOD,DBTAG.METHOD }

            };
    }

    /**
     * A class that represents a DB schema loaded and manipulated by the
     * application. 
     */
    public class DBSchema {

        public DBSchemaNode Schema { get; set; }

      
        public DBSchema() {
            
        }

        public static DBSchema LoadDBSchema(string path) {
            DBSchema result = new DBSchema();
            XmlDocument schemaDoc = new XmlDocument();
            schemaDoc.Load(path);
            DBDevOpsModelBuilderVisitor visitor = new DBDevOpsModelBuilderVisitor();
            DBSchemaNode schema = (DBSchemaNode)visitor.Visit(schemaDoc.DocumentElement);

            result.Schema = schema;

            return result;
        }
    }
    
}
