fixMozillaZIndex=true; //Fixes Z-Index problem  with Mozilla browsers but causes odd scrolling problem, toggle to see if it helps
_menuCloseDelay=500;
_menuOpenDelay=150;
_subOffsetTop=2;
_subOffsetLeft=-2;




with(menuStyle=new mm_style()){
bordercolor="#A1A1A1";
borderstyle="solid";
borderwidth=1;
fontfamily="Verdana, Tahoma, Arial";
fontsize="11pt";
fontstyle="normal";
headerbgcolor="#ffffff";
headercolor="#000000";
offbgcolor="#000000";
offcolor="#FF3399";
onbgcolor="#4863A0";
oncolor="#FF3399";
outfilter="randomdissolve(duration=0.3)";
overfilter="Fade(duration=0.2);Alpha(opacity=90);Shadow(color=#777777', Direction=135, Strength=5)";
padding=5;
pagebgcolor="#000000";
pagecolor="#FF3399";
separatorcolor="#A1A1A1";
separatorsize=1;
subimagepadding=2;
}

with(milonic=new menuname("Main Menu")){
alwaysvisible=1;
followscroll=1;
left=45;
orientation="vertical";
style=menuStyle;
top = 275;
zindex = 80;
aI("text=Home;align=left;url=Default.aspx;");
aI("text=Expanded Duties;align=left;url=ExpandedDuties.aspx;");
aI("text=Dentist Info;align=left;url=DentistInfo.aspx;");
aI("text=Student Profiles;align=left;url=StudentProfiles.aspx;");
aI("text=Gallery;align=left;url=Gallery.aspx;");
aI("text=FAQ's;align=left;url=FAQ.aspx;");
aI("text=About Me;align=left;url=AboutMe.aspx;");
aI("text=Contact Me;align=left;url=ContactMe.aspx");
}

drawMenus();

