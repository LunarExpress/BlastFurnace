   M S B A M L   `   `   `  ����G  CBlastFurnace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  BlastFurnace��5   	   $	��265��6   $	��971��6+    � DataContext 5	   	   ��  �� RelativeSource �� *Self+.��5      �� .+�5   
   �� -   5        $��Import SDF Multiple��5      $��2��6O   $��40��6c   $	��180��6o    �� Left$ 25��6{   
 �� Top$ 28��6�   �� 5   
   -   5        $��Import SDF Template��5      $��40��6P   $	��180��6\   $��2��6h   $ 25��6|   $	 125��6�   � 5   
   -   5         �Name$ NotificationArea��5      $��Right=�6)   $	��Top=�6E   $��0,10,4,0q�6]   $��1��6o   $��0��6   $��0��6�   �� 5   
   -   5        $��Merge!��5      $
��Left=�60   $	��Top=�6K   $��32��6c   $��84��6o   $��1��6z   $	 385��6�   $ 85��6�   �� 5   
   $
��Left=�6   $	��Top=�6-   $	��125��6E   $��1��6Q   $��25��6a   $	 225��6m   $ 35��6   .��5      �� �� 5      -   5         �Name$ Multiple��5   =   $�Status:Empty��6   
���   �6=   5      5      �� 5   
   $��Center=�6   $	��Top=�6/   $	��100��6G   $��1��6S   $��26��6c   $	 225��6o   $	 132��6�   .��5      �� �� 5      -   5        $ Template��5   =   $�Status:Empty��6   
���   �6=   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5        $	��DG1��5      $��1��6      AutoGenerateColumns$
 True��6P   $	��180��6k   $	��428��6x   $
��Left=�6�   $	 491��6�   $ 16��6�   $��Center=�6�   ��6.   �� *	ImageData+6�   �� 5   
   -   5        $��
VersionBox��5      $
��Left=�6%   $��
50,191,0,0q�6@   $	��Top=�6T   $	��120��6l   $��2��6x   �� 5   
   -	   5        $��Usethis��5   !   $��1��6   $��←Use this��6@   $
��Left=�6T   $	��Top=�6o   $��71��6�   $	 225��6�   $	 192��6�   �� 5   
   -
   5        $��SetDatabase��5      $	 367��6;   $	 191��6M   $
��Left=�6^   $��Center=�6y   $��96��6�   5      5       