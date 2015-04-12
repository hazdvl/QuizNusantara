GLUe Framework Free version
by NIC-Starc (www.nic-starc.ru)


This package allows you to create fast user interfaces for Unity in common object-component model.
GLUe intended for creation user interfaces (UI) and HUD's of any complexity.    
Object-oriented model and XML support allows easily separate development between coders and make-up'ers.  


Main features are: 

1. Fast drawing (mesh caching and smart combining wherever possible).  
2. Multy-platform targeting.
3. Low level rich text support.
4. Traditional forms and component model for rapid development.
5. XML support.
6. Only low-level functions are used (GL, Graphics and Input). 
7. Easy forms scaling.
8. Easy widgets creation (each class in GLUe core is non-sealed, sou you can inherit it and extend). 


Notes

This package contains complete project with one example form. You just need to open "Scenes/main" scene and press "Run" button.


Getting started

1. Setting up from empty project:

   1.1 Create an empty project;
   1.2 Create folder "Plugins" in the project (in "Assets" folder);
   1.3 Put glue.dll and glue.xml in "Plugins" folder (if you target platform is mobile, use glue.dll from glue_mobile.rar);
   1.4 Create folder "Resources" and copy there folder "GLU" from "Resources" folder of this package;
   1.5 Copy "GLU" folder from this project to your project's "Assets" folder; 
   1.6 Create new scene and save it;
   1.7 Hook "GLUEngine" script to Main Camera (if you are planning to use deferred lighting path, you should create 
       separate camera for GLUe, remove Audio listener and GUI Layer, set its "Clear flags" to "Don't clear" and "Depth" to 1);    
   1.8 You need to create 5 materails for Text, ScrollView text, Images, Viewports and Outlines (i recommend to use GLU/GLUTextShader 
       for text and GLU/GLUImageShader for others);
   1.9 Set "Font material", "Scrollview font material", "Outline material", "Image material" and "Viewport mesh materials" to 
       materials, created in 1.8
   1.10 GLUe is ready to go. 
   

2. Creating and displaying a new form:

   In GLUe, form is a class, derived from GLUForm. It can contains child controls and have custom skin. Each user action has 
   appropriate delegate to handle it. Everything works in Delphi VCL or WinForms manner.
   
   2.1 Create an empty class YourForm and inherit it from GLUForm;
   2.2 Create constructor, passing parameters to one of inherited constructors (such as size, position, name, text, etc..);
   2.3 In the constructor, put visual controls creation code;
   2.4 Save class;
   2.5 Create new Monobehaviour script and hook it to any active GameObject in scene;
   2.6 In the Start method, create an instance of YourForm class;
   2.7 Call Show() on it.
   
   
3. Importing fonts:
   
   Fonts in GLUe are textures with UV coordinates tables. Symbols are stretched to a polygons, which are combined to meshes. 
   
   3.1 Copy a .ttf file somwhere to you Assets folder and import it;
   3.2 Hook (if not yet) script "GLUFontImporter" to camera with "GLUEngine" script on it;
   3.3 Set "Font" field (of GLUFontImporter) with previously copied font (3.1);
   3.4 Set "Font name" field with font name;
   3.5 Set "Font sizes" array with needed sizes;
   3.6 Press "Run";
   3.7 Press "Import font" button;
   3.8 After import completion, stop the application and reimport "Resources/GLU/Fonts" folder;
   3.9 You can refer to imported font by it's string font name.  
    

Online documentation and support

   Official page - http://nic-starc.ru/dev/GLU/EN/
   SDK - http://nic-starc.ru/dev/GLU/EN/docs/html/index.html
   Demo project - http://nic-starc.ru/dev/GLU/EN/download.php?f=GLUDemo.zip
   Fresh version - http://nic-starc.ru/dev/GLU/EN/download.php?f=GLU.zip
   Online demo - http://nic-starc.ru/dev/GLU/EN/files/presentation/free/WebPlayer.html
   Forums - http://nic-starc.ru/dev/forum/

