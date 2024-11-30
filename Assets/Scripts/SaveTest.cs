using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
    public class ClassesContainer
    {
        public List<MyClass> classes;
    }

    public class MyClass
    {
        public string name = "Alex";
        public int Order => order;
        public int order;
    }

    private XmlVariable<List<MyClass>> xmlSaveHelper;

    private List<MyClass> list;

    private void Start()
    {

    }
}
