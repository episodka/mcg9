   M S B A M L   `   `   `  ����  	YouViewer    YouViewer.CommentWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$   ��5   	   $	��479��6   $	��344��6    � 
ResizeMode$ NoResize=�6+   .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5         ��Name$ commentTxtBox��5      $	��277��6-   �� 5      -   5         ��Name$ postBtn��5      $
��Post��6Z   $��60��6i   {�6:   �� *	IsLogedIn+6i   5      �� 5   
   -   5         ��Name$ commentsListBox��5      $��1��6+   $��Disabled=�68   $
��Auto=�6o   ��6�   �� *ListComment+��5      �� 5      .R�5      �� $ Vertical=�6!   .+�5      �� $
��auto��6$   $
��Bold"�6I   $��11(�6[   $
��Wrap=�6i   ��61   �� *Author+6i   �� 5      $
��auto��6$   $��Italic$�6J   $��8(�6]   $
��Wrap=�6j   ��61   �� *	PubDate+6j   �� 5      $
��auto��6%   $��11(�6K   $
��Wrap=�6Y   ��62   �� *	Content+6Y   5      5      5      5      5      