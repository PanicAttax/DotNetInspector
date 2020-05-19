using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DotNetInspector
{
    class AssemblyInfo
    {
        private Assembly assembly;
        private TypeOfList typeNow;
        private Grid grid;
        private ListBox listBoxMain;


        public Assembly Assembly
        {
            get { return assembly; }
        }
        public TypeOfList TypeNow
        {
            get { return typeNow; }
        }

        public AssemblyInfo(Assembly assembly, Grid grid)
        {
            this.assembly = assembly;
            this.grid = grid;
            listBoxMain = (ListBox)grid.FindName("listBoxMain");
            typeNow = TypeOfList.Types;
        }

        public void Back(IEnumerable itemSource, TypeOfList type)
        {
            typeNow = type;
            listBoxMain.ItemsSource = itemSource;
        }

        public bool CheckListAndRequest(object sender)
        {
            // Initialize clicked button, selected item in the listBox and instance of comparer
            var button = sender as System.Windows.Controls.Button;
            var request = button.Content.ToString().Replace("Get ", "");
            var requestType = (TypeOfList)Enum.Parse(typeof(TypeOfList), request);
            var selectedItem = listBoxMain.SelectedItem;
            var startItemSource = listBoxMain.ItemsSource;
            var comparer = new ItemSourceComparer();

            // Check typeNow to determine what kind of list is in listBox now.
            switch (typeNow)
            {
                // Then check type of request (get types, get methods, etc)
                case TypeOfList.Types:
                    switch (requestType)
                    {
                        case TypeOfList.Types:
                            listBoxMain.ItemsSource = assembly.GetTypes();
                            typeNow = TypeOfList.Types;
                            break;
                        case TypeOfList.Methods:
                            GetMethods(selectedItem as TypeInfo);
                            break;
                        case TypeOfList.Fields:
                            GetFields(selectedItem as TypeInfo);
                            break;
                        case TypeOfList.Properties:
                            GetProperties(selectedItem as TypeInfo);
                            break;
                        case TypeOfList.Constructors:
                            GetConstructors(selectedItem as TypeInfo);
                            break;
                        case TypeOfList.Interfaces:
                            GetInterfaces(selectedItem as Type);
                            break;
                    }
                    break;
                case TypeOfList.Methods:
                    switch (requestType)
                    {
                        case TypeOfList.Types:
                            listBoxMain.ItemsSource = assembly.GetTypes();
                            typeNow = TypeOfList.Types;
                            break;
                    }
                    break;
                case TypeOfList.Fields:
                    switch (requestType)
                    {
                        case TypeOfList.Types:
                            listBoxMain.ItemsSource = assembly.GetTypes();
                            typeNow = TypeOfList.Types;
                            break;
                    }
                    break;
                case TypeOfList.Properties:
                    switch (requestType)
                    {
                        case TypeOfList.Types:
                            listBoxMain.ItemsSource = assembly.GetTypes();
                            typeNow = TypeOfList.Types;
                            break;
                    }
                    break;
                case TypeOfList.Constructors:
                    switch (requestType)
                    {
                        case TypeOfList.Types:
                            listBoxMain.ItemsSource = assembly.GetTypes();
                            typeNow = TypeOfList.Types;
                            break;
                    }
                    break;
                case TypeOfList.Interfaces:
                    switch (requestType)
                    {
                        case TypeOfList.Types:
                            listBoxMain.ItemsSource = assembly.GetTypes();
                            typeNow = TypeOfList.Types;
                            break;
                        case TypeOfList.Methods:
                            GetMethods(selectedItem as TypeInfo);
                            break;
                        case TypeOfList.Fields:
                            GetFields(selectedItem as TypeInfo);
                            break;
                        case TypeOfList.Properties:
                            GetProperties(selectedItem as TypeInfo);
                            break;
                        case TypeOfList.Constructors:
                            GetConstructors(selectedItem as TypeInfo);
                            break;
                    }
                    break;
            }

            // In start of program startItemSource will initialized with null. In this case true will returned
            //if (startItemSource == null)
            //{
            //    return true;
            //}

            // In case when nothing was changed in ItemSource (it equal startItemSource), it no need to be backed up. False will returned.
            return !comparer.Equals(startItemSource, listBoxMain.ItemsSource);
        }




        void GetTypes()
        {
            listBoxMain.ItemsSource = assembly.GetTypes();
            typeNow = TypeOfList.Types;
        }

        void GetMethods(TypeInfo typeInfo)
        {
            if (typeInfo != null)
            {
                listBoxMain.ItemsSource = typeInfo.GetMethods();
                typeNow = TypeOfList.Methods;
            }
        }

        void GetFields(TypeInfo typeInfo)
        {
            if (typeInfo != null)
            {
                listBoxMain.ItemsSource = typeInfo.GetFields();
                typeNow = TypeOfList.Fields;
            }
        }

        void GetProperties(TypeInfo typeInfo)
        {
            if (typeInfo != null)
            {
                listBoxMain.ItemsSource = typeInfo.GetProperties();
                typeNow = TypeOfList.Properties;
            }
        }
        void GetConstructors(TypeInfo typeInfo)
        {
            if (typeInfo != null)
            {
                listBoxMain.ItemsSource = typeInfo.GetConstructors();
                typeNow = TypeOfList.Constructors;
            }
        }

        void GetInterfaces(Type type)
        {
            if (type != null)
            {
                listBoxMain.ItemsSource = type.GetInterfaces();
                typeNow = TypeOfList.Interfaces;
            }
        }
    }
}
