# GamePrototype
A 2D platform game made with Unity.
## Current process  
1.GameCharacter System.  
2.Simple AI script for NPC.  
3.Item System.  
4.Conversation System.  
5.Data System.  
6.Buff System.  
7.Time System.  
8.Survive System.    
9.Equipment System.
## Update  
###  2019/6/3
1.为Gamecharacter基类添加了ItemReference属性，用于实现物品数据缓存.  
2.修改了Data中部分序列化处理机制.  
###  2019/6/10
1.添加了关于GameCharacter基类的Editor扩展，用于调试时调配游戏资源.
###  2019/6/22
1.更新Unity版本至2019.2b6，引擎中加入了2D渲染器，可以使用2D光源，但还没有提供阴影功能.
###  2019/8/12
1.优化存储机制，现在Item对象可以存储更多细节信息.  
2.新增世界时间系统.  
3.新增Buff系统.  
4.新增生存系统，引入了饥饿值，并且大部分行动现在都需要消耗体力值.  
###  2019/8/20
1.新增装备系统.  
2.实现了物品栏、快捷栏、装备栏之间的物品拖拽.  
3.修复了ToxicTrap生效过程中，MouseInfo界面不能正常隐藏的Bug.  
4.优化存储机制，现在可以正确地在快捷栏与装备栏登记物品.  
5.为玩家添加了部分行动音效.  
