   M S B A M L   `   `   `  ����H  DTMSMainWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TMSMainWindowTMSMainWindow  5   	   #    TMSMainWindow.BuyerMainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:TMSMainWindow     5   	     � Title$  BuyerMainWindow��5   	   $	��720��6!   $
��1280��6.   .��5	      � .+�5
   
   � $
��Left=�6   $	��689��6*   $	��Top=�67   $	��420��6O   .+�5      �� -   5        $��NewCustomerListBox��5      $
��Left=�60   $	��259��6K   $��	10,68,0,0q�6X   $	��Top=�6k   $	��400��6�   {�.  6�   �� 5      -   5        $��AcceptedCustomerListBox��5      $
��Left=�65   $	��231��6P   $��
10,448,0,0q�6]   $	��Top=�6q   $	��400��6�   {�.  6�   �� 5      -   5        $"��AcceptSelectedCustomerButton��5      $��18(�69   $��Accept selected customer��6G   $
��Left=�6j   $��
86,350,0,0q�6�   $	��Top=�6�   $	��248��6�   $��40��6�   {�.  6�   �� 5      $��New Customers��6   $��30(�6,   $
��Left=�6:   $��	10,13,0,0q�6U   $	��Top=�6h   �� 5      $��Accepted Customers��6   $��30(�61   $
��Left=�6?   $��
10,395,0,0q�6Z   $	��Top=�6n   �� 5      $��Marketplace Orders��6   $��30(�61   $
��Left=�6?   $��432,13,-293,0q�6Z   $	��Top=�6q   $	��281��6�   5      � 5   
   $
��Left=�6   $	��689��6*   $	��Top=�67   $	��420��6O   $��	420,0,0,0q�6[   .+�5      �� -   5        $��MarketplaceOrdersListBox��5      $
��Left=�66   $	��450��6Q   $��	10,68,0,0q�6^   $	��Top=�6q   $	��400��6�   5      �� 5      -   5        $��CreateNewOrderButton��5      {�.  $��18(�6C   $$��Create new order from selected��6Q   $
��Left=�6z   $��
74,631,0,0q�6�   $	��Top=�6�   $	��266��6�   $��40��6�   6�   �� 5      -   5        $��SelectCityToAddDropdown��5      $
��Left=�66   $��
10,574,0,0q�6Q   $	��Top=�6e   $	��266��6}   $��32��6�   �� 5      -   5        $��AddCityToOrderButton��5      $��18(�61   $	��Add��6?   $
��Left=�6M   $��310,574,0,0q�6h   $	��Top=�6}   $	��100��6�   $��32��6�   6�   �� 5      $��18(�6   $��Select addtional stops...��6"   $
��Left=�6F   $��
10,535,0,0q�6a   $	��Top=�6u   5      � 5   
   $
��Left=�6   $	��689��6*   $	��Top=�67   $	��432��6O   $��	840,0,0,0q�6[   .+�5      �� -   5        $��GenerateInvoiceButton��5      $��18(�62   $)��#Generate invoice for selected order��6@   $
��Left=�6n   $��
66,630,0,0q�6�   $	��Top=�6�   $	��304��6�   $��40��6�   6�   �� 5       -	   5        $��CompletedOrdersListBox��5       $
��Left=�64   $	��538��6O   $��	10,68,0,0q�6\   $	��Top=�6o   $	��412��6�   �� 5!      $��Completed Orders��6   $��30(�6/   $
��Left=�6=   $��	10,13,0,0q�6X   $	��Top=�6k   $	��281��6�   5"      5#      5$      