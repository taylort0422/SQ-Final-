   M S B A M L   `   `   `  ����d  `Microsoft.Maps.MapControl.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tRclr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPFMicrosoft.Maps.MapControl.WPF  5   	   H DTMSMainWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TMSMainWindowTMSMainWindow 5   	   %   TMSMainWindow.PlannerMainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   `mRclr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPF     5   	   -localclr-namespace:TMSMainWindow     5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � Title$  PlannerMainWindow��5   	   $	��720��6#   $
��1280��60   .��5      � .+�5   
   � $
��Left=�6   $	��689��6*   $	��Top=�67   $	��722��6O   .+�5      �� $��30(�6   $��Active Orders��6"   $
��Left=�6:   $��	10,10,0,0q�6U   $	��Top=�6h   �� 5      -   5        $��ActiveOrdersDataGrid��5      $
��Left=�62   $	��274��6M   $��	10,60,0,0q�6Z   $	��Top=�6m   $	��702��6�   6�   �� 5      -   5         ��Name$ CarrierStack��5      .+�5      �� -   5        $��CarrierListBox��5      $
��Left=�60   $	��259��6K   $��
10,420,0,0q�6X   $	��Top=�6l   $	��250��6�   6�   5      �� 5      $��30(�6   $��Modify Selected Order��6"   $
��Left=�6B   $��
10,339,0,0q�6]   $	��Top=�6q   �� 5      $��16(�6   $��Carriers��6"   $
��Left=�65   $��
10,389,0,0q�6P   $	��Top=�6d   �� 5      $��16(�6   $'��!Trips currently attached to Order��6"   $
��Left=�6N   $��462,386,0,0q�6i   $	��Top=�6~   �� 5      -   5        $��AddTripToOrderButton��5      $
��Left=�61   $��265,453,0,0q�6L   $	��Top=�6a   $	��192��6y   $��56��6�   .��5      �� $��16(�6   $
��Wrap=�6*    �� TextAlignment$ Center=�6>   �� 6V   $�Add Trip to Order��6Z   �� 6u   �� 6�   $
�🡆��6�   6�   5      �� 5      -   5        $!��TripsAttachedToOrderListBox��5      $
��Left=�69   $	��259��6T   $��462,417,0,0q�6a   $	��Top=�6v   $	��250��6�   �� 5      -   5        $��RemoveTripFromOrderButton��5      $
��Left=�66   $��265,523,0,0q�6Q   $	��Top=�6f   $	��192��6~   $��57��6�   .��5      �� $��16(�6   $
��Wrap=�6*   $ Center=�6>   $��Remove Trip from Order��6U   5      �� 5      -   5        $��MarkOrderAsCompleteButton��5      $
��Left=�66   $��265,596,0,0q�6Q   $	��Top=�6f   $	��192��6~   $��56��6�   {�.  .��5      �� $��16(�6   $
��Wrap=�6*   $ Center=�6>   �� 6V   $�Mark Order as Complete��6Z   6{   5      5       � 5!   
   $
��Left=�6   $	��689��6*   $��	727,0,0,0q�67   $	��Top=�6J   $	��545��6b   .+�5"      '   !Microsoft.Maps.MapControl.WPF.Map  -   5        $	��Map��5"      +   %Microsoft.Maps.MapControl.WPF.MapCore   CredentialsProviderT   NMicrosoft.Maps.MapControl.WPF.Design.ApplicationIdCredentialsProviderConverter$F @AmC2NIVH6Hki-5mUPviE1KwWHwr5-iWsm1R4j2zxo18OPRQg7iks3P1G14SB7TxH 6      Center<   6Microsoft.Maps.MapControl.WPF.Design.LocationConverter$ 43.430038, -79.691651 6v      	ZoomLevel$ 6Q�6�      Mode;   5Microsoft.Maps.MapControl.WPF.Design.MapModeConverter$
 Road 6�   $��10,60,10,84q�6�      Children. 5#      +   %Microsoft.Maps.MapControl.WPF.Pushpin  -	   5        $��Hamilton��5#   =      Location$ 43.257986, -79.876444 6   $��Hamilton��6M   $y�	Collapsed=�6`     5$      -
   5        $��London��5$   =   $ 42.986900, -81.246220 6   $��London��6K   $y�	Collapsed=�6\     5%      -   5        $��Windsor��5%   =   $ 42.316581, -83.031123 6   $��Windsor��6L   $y�	Collapsed=�6^     5&      -   5        $��Oshawa��5&   =   $ 43.894904, -78.863242 6   $��Oshawa��6K   $y�	Collapsed=�6\     5'      -   5        $��Toronto��5'   =   $ 43.650687, -79.384410 6   $��Toronto��6L   $y�	Collapsed=�6^     5(      -   5        $��
Belleville��5(   >   $ 44.2405145,-77.5012222 6   $��
Belleville��6P   $y�	Collapsed=�6e     5)      -   5        $��Kingston��5)   >   $ 44.3397743,-76.6132777 6   $��Kingston��6N   $y�	Collapsed=�6a     5*      -   5        $��Ottawa��5*   =   $ 45.421780, -75.691190 6   $��Ottawa��6K   $y�	Collapsed=�6\   5+      �� 5,      $��30(�6   $	��Map��6"   $
��Left=�60   $��	10,10,0,0q�6K   $	��Top=�6^   �� 5-      $��18(�6   $��Pass           days��6#   $
��Left=�6A   $��
59,610,0,0q�6\   $	��Top=�6p   �� 5.      $��18(�6   $��Generate reports for...��6#   $
��Left=�6E   $��323,610,0,0q�6`   $	��Top=�6u   �� 5/      -   5        $"��GenerateInvoiceAllTimeButton��5/   #   $��16(�6   $��All time��6G   $
��Left=�6Z   $��297,644,0,0q�6u   $	��Top=�6�   $	��114��6�   $��35��6�   6�   �� 50      -   5        $%��GenerateInvoicePast2WeeksButton��50   #   $��16(�6   $��Past 2 weeks��6J   $
��Left=�6a   $��421,644,0,0q�6|   $	��Top=�6�   $	��114��6�   $��35��6�   6�   �� 51      -   5        $��PassTimeButton��51   #   $��16(�6   $	��Go!��69   $
��Left=�6G   $��
69,644,0,0q�6b   $	��Top=�6v   $	��114��6�   $��35��6�   6�   %  Xceed.Wpf.Toolkit.IntegerUpDown  52      -   5        $%��NumberOfDaysToPassIntegerUpDown��52   8   ,  &Xceed.Wpf.Toolkit.Primitives.InputBase	   Text$	 2��6!   $��16(�6*   $��103,615,398,50q�6_   �	  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
 	  Minimum$
 0X�6w      AllowTextInput .  6�   53      54      55      