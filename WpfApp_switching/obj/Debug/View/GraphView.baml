   M S B A M L   `   `   `  ����  WpfApp_switching%    WpfApp_switching.View.GraphView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  5      
�������.��5
      �   � RowDefinitions  5   
   �� 5      $��4* �6   �� 5      $��4* �6   �� 5      $��1* �6    � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��4.75* �6   �� 5      $��0.25* �6   .+�5   
     OxyPlot.Wpf.PlotView  -   5          Name$ DataPlotView��5      $��0��5      $��1��6$      Model 5      �� *Plot1+  5   
   -   5        $ DataPlotView2��5      $��1��5      $��1��6$    5      �� *Plot2+� 5"   
   -   5         �Name$ 	Plot_Menu��5#      $��0��5"      $��0��6   $��3��6,   
����d�$
��Auto��5$   +     5%      �� 5&      $
��Auto �6    �� 5'      $
��Auto �6    �� 5(      $
��Auto �6    �� 5)      $
��Auto �6    .+�5+      �� $��0��5,      $��30��5-      $��70��5.      $��
30,10,0,10q�5/      $
��Left=�50      ��51      �� *
StartBtn+.��6.   START53      �� 54      $��1��55      $��30��56      $��70��57      $��
30,10,0,10q�58      $
��Left=�59      ��5:      �� *	StopBtn+.��6-   STOP5<      5>      �� 5A   
   $��2��5B      $��1��5C      $��
20,10,0,10q�5D      $��IP address: ��5E      $��Center=�5F      �� 5G   
   $��2��5H      $��1��5I      $��
80,10,0,10q�5J      $	��100��5K      $��20��5L      $
��Left=�5M      ��5N      �� *	IpAddress+�� 5O   
   $��2��5P      $��1��5Q      $��0,10,100,10q�5R      $��Sample time [ms]: ��5S      $��Center=�5T      $��Right=�5U      �� 5V   
   $��2��5W      $��1��5X      $��
0,10,20,10q�5Y      $��80��5Z      $��20��5[      $��Right=�5\      ��5]      �� *
SampleTime+5^      5_      