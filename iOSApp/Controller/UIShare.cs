using CoreGraphics;
using System;
using UIKit;

namespace iOSApp
{
    //扩展方法
    public static class UIExtension
    {
        //创建UILabel
        public static UILabel CreateLabel(this UIView view, nfloat x, nfloat y, string text, int lines = 1, UITextAlignment textAlignment = UITextAlignment.Left)
        {
            CGRect frame = new CGRect(x, y, view.Bounds.Width - x * 2, 25 * lines);

            UILabel label = new UILabel(frame);
            label.Text = text;
            label.Lines = lines;
            label.TextAlignment = textAlignment;
            //如果父控件尺寸变了，自动调整本控件宽度，保持左右边距，自适应父控件尺寸
            label.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            view.AddSubview(label);

            return label;
        }

        //创建UIButton
        //UIColor.Black默认参数不行，报错：必须编译时是常量
        //public static UIButton CreateButton(this UIView view, nfloat x, nfloat y, string title,UIColor titleColor= UIColor.Black, UIColor backgroundColor= UIColor.LightGray)
        public static UIButton CreateButton(this UIView view, nfloat x, nfloat y, string title)
        {
            //蓝底白字
            UIButton button = CreateButton(view, x, y, title, UIColor.White, UIColor.FromRGBA(0, 128, 255, 255));

            return button;
        }

        public static UIButton CreateButton(this UIView view, nfloat x, nfloat y, string title, UIColor titleColor, UIColor backgroundColor)
        {
            CGRect frame = new CGRect(x, y, view.Bounds.Width - x * 2, 25);

            UIButton button = new UIButton(UIButtonType.RoundedRect);//圆角
            button.Frame = frame;
            button.Layer.CornerRadius = 5;//圆角
            button.BackgroundColor = backgroundColor;
            button.SetTitle(title, UIControlState.Normal);
            button.SetTitleColor(titleColor, UIControlState.Normal);
            //如果父控件尺寸变了，自动调整本控件宽度，保持左右边距，自适应父控件尺寸
            button.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            view.AddSubview(button);

            return button;
        }

        //创建UITextField
        public static UITextField CreateTextField(this UIView view, nfloat x, nfloat y, string placeholder = "", int lines = 1)
        {
            //CGRect frame = new CGRect(x, y, view.Bounds.Width - x * 2, 30);
            CGRect frame = new CGRect(x, y, view.Bounds.Width - x * 2, 25 * lines);

            UITextField textField = new UITextField(frame);
            textField.BorderStyle = UITextBorderStyle.RoundedRect;
            //未填写时的提示文本
            textField.Placeholder = placeholder;
            //编辑时显示交叉符号，点击可以清除全部输入
            textField.ClearButtonMode = UITextFieldViewMode.WhileEditing;
            //键盘的RETURN键为DONE
            textField.ReturnKeyType = UIReturnKeyType.Done;
            //点击键盘的RETURN键，消除键盘
            textField.ShouldReturn = (tf) =>
            {
                tf.ResignFirstResponder();//消除键盘
                return true;
            };
            //如果父控件尺寸变了，自动调整本控件宽度，保持左右边距，自适应父控件尺寸
            textField.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            view.AddSubview(textField);

            return textField;
        }

        //创建弹出窗口
        public static UIAlertController ShowMessageBox(this UIViewController viewController, string title, string content, UIAlertAction[] alertActions = null)
        {
            //创建弹出窗口
            UIAlertController alertController = UIAlertController.Create(title, content, UIAlertControllerStyle.Alert);

            //默认只有一个确定按钮
            if (alertActions == null)
                alertActions = new UIAlertAction[] { UIAlertAction.Create("确定", UIAlertActionStyle.Default, null) };

            //创建按钮
            foreach (UIAlertAction alertAction in alertActions)
            {
                alertController.AddAction(alertAction);
            }

            //显示弹出窗口
            viewController.PresentViewController(alertController, true, null);

            return alertController;
        }

        //创建弹出多个按钮窗口
        public static UIAlertController ShowSheetBox(this UIViewController viewController, string title, string content, UIAlertAction[] alertActions)
        {
            //创建弹出窗口
            UIAlertController alertController = UIAlertController.Create(title, content, UIAlertControllerStyle.ActionSheet);

            //如果在iPad上运行，要设置弹出此窗口的位置信息
            if (alertController.PopoverPresentationController != null)
                alertController.PopoverPresentationController.SourceView = viewController.View as UIView;

            //创建按钮
            foreach (UIAlertAction alertAction in alertActions)
            {
                alertController.AddAction(alertAction);
            }

            //显示弹出窗口
            viewController.PresentViewController(alertController, true, null);

            return alertController;
        }

    }
}