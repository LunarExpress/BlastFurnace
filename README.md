
# BlastFurnace
A Unity localization tool,For replacing TextmeshPro SDF file.


How to use: Export the dump text that supports the target language from the Unity packaged game of the same version, use uabea, and import it into the program as a Template;
Import multiple font dump text to be modified into Multiple, and then click Merge.
Then the font to be modified can be directly imported into the game to realize Chinese (or other language) support, without manually modifying pathid.


使用方法：把支持目标语言的dump text从同版本的Unity打包游戏中，使用uabea导出，并作为Template导入该程序；
把要修改的多个字体dump text导入Multiple，随后点击Merge。
那么要修改的字体就可以直接导入游戏里，实现中文（或者其他语言的支持），无需手动修改pathid。

23/7/20更新:现在可以自定义字体库了.使用Set Database按钮可以读取指定目录下的文件夹.例如我设置E:/Db 为目录,那么下拉框里就会出现E:/Db目录下的所有文件夹的名字.
用于替换的SDF字体应该作为唯一一个txt存在于文件夹内.建议把文件夹的名字设置为版本号方便处理.
非常感谢https://github.com/baiyanzhao/FolderBrowserForWPF 的作者,没想到WPF给我留了这么大一个坑.
