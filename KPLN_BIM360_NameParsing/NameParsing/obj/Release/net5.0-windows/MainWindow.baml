   M S B A M L   `   `   `  ���� F  BNameParsing, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null)clr-namespace:NameParsingNameParsing      NameParsing.MainWindow   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:NameParsing       � Title$  KPLN. BIM360��$	��475��$	��600�� � 	Resources  �� 
TargetType �� Property �� Value��%�  '��       '�� �     �� " ��.���  � ��$ Times New Roman)���  � ��$ Stretch=���  � ��$ Center=���  � ��$
 Wrap=���  � ��$ 16(���  � ��$ 	10,5,10,5q��� " ��.���  � ��$ Times New Roman)���  � ��$ Stretch=���  � ��$ Center=���  �@��TextWrapping$
 Wrap=���  � ��$ 16(���  � ��$ 	10,5,10,5q�.��� .+��� $
��Auto=�.���� .+��� �� .�Укажите �� $
��Bold"�.�email �� .�*(для авторизации в BIM360:�� -    ��Name$ emailDef��$��5,0,5,0q��� �� .�Укажите �� $
��Bold"�.�пароль �� .�*(для авторизации в BIM360:G� -    G�Name$ passwDef��$��5,0,5,0q��� �� .�Скопируйте �� $
��Bold"�.�$"ссылку из браузера �� .�/-на нужную страницу в BIM360:�� -   $ urlDef��$��5,0,5,0q� �� TextWrapping$
 Wrap=��� �� .�Скопируйте �� $
��Bold"�.�$"путь из проводника �� .�на нужную папку:�� -   $ dirDef��$��5,0,5,0q�$
 Wrap=��� $*��$Скорость интернета:���  � RowDefinitions �� $��25 px �	 � ColumnDefinitions	 �� �� .+��� $��0��$��0��$	��MIN���� $��1��$��0��$	��MAX��$��Right=��� -   
 ��Name$
 slider�� � Minimum$	 750Q� � Maximum$
 2500Q�$��30�� �� IsDirectionReversed$
 True��$��5q�� -    �Name$ ExtCheckBoxes�� �� $��20 px ��� $��20 px �	 �� �� .+��� -    ��Name$ rvtCheckBox��$��0��$��0��$��20��$	��RVT��$��
5, 0, 0, 0q��� -   $ pdfCheckBox��$��1��$��0��$��20��$	��PDF��$��
5, 0, 0, 0q��� -	   $ dwgCheckBox��$��0��$��1��$��20��$	��DWG��$��
5, 0, 0, 0q��� -
    ��Name$ btnStrt��$��60��$��30��$��
Старт��� -   $
��pBar��$ 0Q�$	 100Q�$��25��$��5, 10, 5, 0q�