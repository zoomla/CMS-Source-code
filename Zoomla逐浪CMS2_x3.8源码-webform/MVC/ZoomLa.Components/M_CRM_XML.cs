using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Components
{
    [Serializable]
    class M_CRM_XML
    {
       private int m_sort;
       private bool m_default_;
       private bool m_enalbe;
       private string m_content;

       public int sort {
           get { return this.m_sort; }
           set { this.m_sort=value; }
       }

       public bool default_
       {
           get { return this.m_default_; }
           set { this.m_default_=value; }
       }

       public bool enalbe
       {
           get { return this.m_enalbe; }
           set {  this.m_enalbe=value; }
       }

       public string content
       {
           get { return this.m_content; }
           set {  this.m_content = value; }
       }
    }
}
