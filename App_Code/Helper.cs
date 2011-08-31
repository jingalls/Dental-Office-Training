using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;

namespace App_Code
{
    /// <summary>
    /// Summary description for Helper
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// 
        /// </summary>
        public enum Month
        {
            /// <summary>
            /// 
            /// </summary>
            January = 1,
            /// <summary>
            /// 
            /// </summary>
            February = 2,
            /// <summary>
            /// 
            /// </summary>
            March = 3,
            /// <summary>
            /// 
            /// </summary>
            April = 4,
            /// <summary>
            /// 
            /// </summary>
            May = 5,
            /// <summary>
            /// 
            /// </summary>
            June = 6,
            /// <summary>
            /// 
            /// </summary>
            July = 7,
            /// <summary>
            /// 
            /// </summary>
            August = 8,
            /// <summary>
            /// 
            /// </summary>
            September = 9,
            /// <summary>
            /// 
            /// </summary>
            October = 10,
            /// <summary>
            /// 
            /// </summary>
            November = 11,
            /// <summary>
            /// 
            /// </summary>
            December = 12,
        }

        /// <summary>
        /// Loads the DOT classes.
        /// </summary>
        /// <returns></returns>
        public static List<DOTClass> LoadDOTClasses()
        {
            List<DOTClass> classes = new List<DOTClass>();

            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("StudentProfiles.xml"));

            const string xpath = "studentprofiles";
            XmlNode node = doc.SelectSingleNode(xpath);

            if (node != null)
            {
                foreach(XmlNode classNode in node.ChildNodes)
                {
                    DOTClass dotClass = new DOTClass();
                    List<StudentProfile> profiles = new List<StudentProfile>();
                
                    dotClass.Period = classNode.Attributes.GetNamedItem("period").Value;

                    XmlNode dayNode = classNode.SelectSingleNode("day");

                    if (dayNode != null)
                    {
                        dotClass.DayClassImage = dayNode.Attributes.GetNamedItem("image").Value;

                        foreach (XmlNode childNode in dayNode.ChildNodes)
                        {
                            StudentProfile profile = new StudentProfile
                                                     {
                                                         ClassTime = "Day",
                                                         Name = childNode.Attributes.GetNamedItem("name").Value,
                                                         Image = childNode.Attributes.GetNamedItem("image").Value,
                                                         Hometown = childNode.Attributes.GetNamedItem("hometown").Value,
                                                         Testimonial = childNode.Attributes.GetNamedItem("testimonial").Value
                                                     };

                            profiles.Add(profile);
                        }
                    }

                    XmlNode eveningNode = classNode.SelectSingleNode("evening");

                    if (eveningNode != null)
                    {
                        dotClass.EveningClassImage = eveningNode.Attributes.GetNamedItem("image").Value;

                        foreach (XmlNode childNode in eveningNode.ChildNodes)
                        {
                            StudentProfile profile = new StudentProfile
                                                     {
                                                         ClassTime = "Evening",
                                                         Name = childNode.Attributes.GetNamedItem("name").Value,
                                                         Image = childNode.Attributes.GetNamedItem("image").Value,
                                                         Hometown = childNode.Attributes.GetNamedItem("hometown").Value,
                                                         Testimonial = childNode.Attributes.GetNamedItem("testimonial").Value
                                                     };

                            profiles.Add(profile);
                        }
                    }

                    dotClass.Students = profiles;
                    classes.Add(dotClass);
                }
            }

            return classes;
        }

        /// <summary>
        /// Loads the DOT class.
        /// </summary>
        /// <param name="classToLoad">The class to load.</param>
        /// <returns></returns>
        public static DOTClass LoadDOTClass(string classToLoad)
        {
            DOTClass dotClass = new DOTClass();
            List<StudentProfile> profiles = new List<StudentProfile>();

            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("StudentProfiles.xml"));

            string xpath = "studentprofiles/class[@period='" + classToLoad + "']";
            XmlNode node = doc.SelectSingleNode(xpath);

            if (node != null)
            {
                dotClass.Period = node.Attributes.GetNamedItem("period").Value;

                XmlNode dayNode = node.SelectSingleNode(xpath + "/day");
                if (dayNode != null)
                {
                    dotClass.DayClassImage = dayNode.Attributes.GetNamedItem("image").Value;

                    foreach (XmlNode childNode in dayNode.ChildNodes)
                    {
                        StudentProfile profile = new StudentProfile
                                                 {
                                                     ClassTime = "Day",
                                                     Name = childNode.Attributes.GetNamedItem("name").Value,
                                                     Image = childNode.Attributes.GetNamedItem("image").Value,
                                                     Hometown = childNode.Attributes.GetNamedItem("hometown").Value,
                                                     Testimonial = childNode.Attributes.GetNamedItem("testimonial").Value
                                                 };

                        profiles.Add(profile);
                    }
                }

                XmlNode eveningNode = node.SelectSingleNode(xpath + "/evening");
                if (eveningNode != null)
                {
                    foreach (XmlNode childNode in eveningNode.ChildNodes)
                    {
                        StudentProfile profile = new StudentProfile
                                                 {
                                                     ClassTime = "Evening",
                                                     Name = childNode.Attributes.GetNamedItem("name").Value,
                                                     Image = childNode.Attributes.GetNamedItem("image").Value,
                                                     Hometown = childNode.Attributes.GetNamedItem("hometown").Value,
                                                     Testimonial = childNode.Attributes.GetNamedItem("testimonial").Value
                                                 };

                        profiles.Add(profile);
                    }
                }

                dotClass.Students = profiles;
            }

            return dotClass;
        }

        /// <summary>
        /// Builds the class header control.
        /// </summary>
        /// <param name="classData">The class data.</param>
        /// <returns></returns>
        public static Control BuildClassHeaderControl(DOTClass classData)
        {
            Table table = new Table();
	        table.ID = "tblHeader" + classData.Period;
            table.Style.Add("width", "100%");

            TableRow row = new TableRow();
            TableCell tdDay;
            TableCell tdEvening;

            if (!string.IsNullOrEmpty(classData.DayClassImage))
            {
                tdDay = new TableCell();
                tdDay.Style.Add("text-align", "center");

                Image img1 = new Image { ImageUrl = "~/images/" + classData.DayClassImage };

                LiteralControl lit1 = new LiteralControl { Text = "<br />Day Class" };

                tdDay.Controls.Add(img1);
                tdDay.Controls.Add(lit1);
                row.Cells.Add(tdDay);
            }

            if (!string.IsNullOrEmpty(classData.EveningClassImage))
            {
                tdEvening = new TableCell();
                tdEvening.Style.Add("text-align", "center");

                Image img2 = new Image { ImageUrl = "~/images/" + classData.EveningClassImage };

                LiteralControl lit2 = new LiteralControl { Text = "<br />Evening Class" };

                tdEvening.Controls.Add(img2);
                tdEvening.Controls.Add(lit2);
                row.Cells.Add(tdEvening);
            }

            foreach (TableCell cell in row.Cells)
            {
                cell.Style.Add("width", (100 / row.Cells.Count) + "%");
            }

            table.Rows.Add(row);

            return table;
        }

        /// <summary>
        /// Builds the class content control.
        /// </summary>
        /// <param name="classData">The class data.</param>
        /// <returns></returns>
        public static Control BuildClassContentControl(DOTClass classData)
        {
            Table table = new Table();
            table.ID = "tblContent" + classData.Period;
            table.Style.Add("width", "100%");
            table.CellPadding = 5;

            TableRow row = null;
            int count = 0;

            foreach (StudentProfile profile in classData.Students)
            {
                if ((count % 5) == 0)
                {
                    if (row != null)
                        table.Rows.Add(row);

                    row = new TableRow();
                }

                TableCell td1 = new TableCell { CssClass = "ProfileImg" };

                Image studentImg = new Image { ImageUrl = "~/images/" + profile.Image };

                LiteralControl name = new LiteralControl { Text = "<br />" + profile.Name };

                LiteralControl homeTown = new LiteralControl { Text = "<br />" + profile.Hometown };

                td1.Controls.Add(studentImg);
                td1.Controls.Add(name);
                td1.Controls.Add(homeTown);
                if (row != null)
                {
                    row.Cells.Add(td1);
                }

                count++;
            }

            if(row != null)
                table.Rows.Add(row);

            return table;
        }

        /// <summary>
        /// Gets the thumbnail images for the picture gallery page
        /// </summary>
        /// <returns>A List of GalleryRow containing thumbnail images</returns>
        public static List<GalleryRow> GetGalleryThumbnails()
        {
            List<GalleryRow> images = new List<GalleryRow>();

            DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("images/Gallery"));

            if (!dir.Exists) return null;

            List<FileInfo> directoryImages = new List<FileInfo>(dir.GetFiles("*.jpg"));
            var imagesWithThumbNails = from img in directoryImages
                                       where img.Name.Contains("_100")
                                       select img;

            var imagesWithOutThumbNails = from img in directoryImages
                                          where !img.Name.Contains("_100")
                                          join thumbNails in imagesWithThumbNails on img.Name equals thumbNails.Name.Replace("_100", "") into thmbs
                                          from i in thmbs.DefaultIfEmpty()
                                          where i == null
                                          select img;

            directoryImages = new List<FileInfo>();
            directoryImages.AddRange(imagesWithThumbNails);
            directoryImages.AddRange(imagesWithOutThumbNails);

            GalleryRow row = new GalleryRow();

            for (int i = 0; i < directoryImages.Count; i++)
            {
                if (i != 0 && i % 4 == 0)
                {
                    images.Add(row);
                    row = new GalleryRow();
                }

                row.Columns.Add(new GalleryColumn(directoryImages[i]));
            }

            images.Add(row);

            return images;
        }

        /// <summary>
        /// Gets the images for the picture gallery page
        /// </summary>
        /// <returns>A list of fileinfo containing images</returns>
        public static List<GalleryRow> GetGalleryImages()
        {
            List<GalleryRow> rows = new List<GalleryRow>();

            DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("images/Gallery"));

            if (!dir.Exists) return null;

            List<FileInfo> images = dir.GetFiles("*.jpg").Where(fi => !fi.Name.Contains("_")).ToList();

            GalleryRow row = new GalleryRow();

            for (int i = 0; i < images.Count; i++)
            {
                if (i != 0 && i % 4 == 0)
                {
                    rows.Add(row);
                    row = new GalleryRow();
                }

                row.Columns.Add(new GalleryColumn(images[i]));
            }

            rows.Add(row);

            return rows;
        }

        /// <summary>
        /// Gets the first day of month.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DayOfWeek GetFirstDayOfMonth(DateTime date)
        {
            DateTime dt = new DateTime(date.Year, date.Month, 1);
            return dt.DayOfWeek;
        }

        /// <summary>
        /// Gets the last day of month.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(DateTime date)
        {
            DateTime dt = new DateTime(date.Year, date.Month, 1);
            dt.AddMonths(1).AddDays(-1.0);
            return dt;
        }

        /// <summary>
        /// Nums the of days in month.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static int NumOfDaysInMonth(DateTime date)
        {
            int days = 0;
            int year = date.Year;
            bool IsLeap = (((year % 4) == 0) && ((year % 100) != 0) || ((year % 400) == 0));

            switch ((Month)date.Month)
            {
                case Month.January:
                    days = 31;
                    break;
                case Month.February:
                    days = IsLeap ? 29 : 28;
                    break;
                case Month.March:
                    days = 31;
                    break;
                case Month.April:
                    days = 30;
                    break;
                case Month.May:
                    days = 31;
                    break;
                case Month.June:
                    days = 30;
                    break;
                case Month.July:
                    days = 31;
                    break;
                case Month.August:
                    days = 31;
                    break;
                case Month.September:
                    days = 30;
                    break;
                case Month.October:
                    days = 31;
                    break;
                case Month.November:
                    days = 30;
                    break;
                case Month.December:
                    days = 31;
                    break;
            }

            return days;
        }
    }
}