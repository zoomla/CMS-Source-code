(function ($) {
    //�趨���ͣ����λ��
    $.fn.setCursorPosition = function (position) {
        if (!this) {
            return;
        }
        return $(this).setSelection(position, position);
    }

    $.fn.setSelection = function (selectionStart, selectionEnd) {
        if (!this) {
            return;
        }
        input = this[0];

        if (input.createTextRange) {
            var range = input.createTextRange();
            range.collapse(true);
            range.moveEnd('character', selectionEnd);
            range.moveStart('character', selectionStart);
            range.select();
        } else if (input.setSelectionRange) {
            input.focus();
            input.setSelectionRange(selectionStart, selectionEnd);
        }

        return this;
    }

    //����궨λ��¼�������
    $.fn.focusEnd = function () {
        this.setCursorPosition(this.val().length);
    }

    //��ȡһ���ı���������ݣ�showCountId��ʾ����״̬�Ķ��󣬽�ȡ���������Ƿ��Զ����֣��Ƿ�����Ӧ�߶�
    $.fn.GetTextCount = function (showCountId, countNum, isCut, isAutoHeight) {
        if (!this) {
            return;
        }
        if (isAutoHeight) {
            this.autoHeight();
        }

        this.focus(function () {
            setTimeOutGetTextCount(this, showCountId, countNum, isCut, true);
        });
    }

    //ѭ����������
    function setTimeOutGetTextCount(element, showCountId, countNum, isCut, isContinue) {
        var $textCount = $(element).val().length;
        $("#"+showCountId).html($textCount + "/" + countNum);
        if (isCut) {
            if ($textCount > countNum) {
                element.value = element.value.slice(0, countNum);
                $(element).focusEnd();
            }
        }
        if (isContinue) {
            setTimeout(function () { setTimeOutGetTextCount(element, showCountId, countNum, isCut, document.activeElement == element) }, 300);
        }
    }

    //����Ӧ�߶�
    $.fn.autoHeight = function (maxHeight) {
        if (!this) {
            return;
        }
        $this = this[0];

        if (!$this) {
            return;
        }

        $this.style.overflow = 'hidden';

        this.focus(function () {
            if (!$(this).data("height")) {
                $(this).data("height", this.scrollHeight);
            }
            if (!maxHeight) {
                maxHeight = 200;
            }
            minHeight = $(this).data("height")+10;
            setTimeoutAutoHeight(this, maxHeight, minHeight);
        });
    }

    //����¼���ĸ߶�
    function SetTextAreaHeight(element, maxHeight, minHeight) {
        $(element).parent().css("height", $(element).css("height"));
        element.style.height = $(element).data("height") + 'px';

        $textareaHeight = element.scrollHeight;

        if (minHeight && $textareaHeight < minHeight) {
            $textareaHeight = minHeight;
        }
        if (maxHeight && $textareaHeight > maxHeight) {
            $textareaHeight = maxHeight;
        }
        element.style.height = $textareaHeight + 'px';
        if ($textareaHeight >= maxHeight) {
            element.style.overflow = "auto";
        } else {
            element.style.overflow = 'hidden';
        }
        $(element).parent().css("height", $(element).css("height"));
    }

    //ѭ������Ӧ�߶�
    function setTimeoutAutoHeight(element, maxHeight, minHeight, lastTextCount) {
        var $textCount = $(element).val().length;
        $textCount = $textCount % 1 != 0 ? $textCount + 0.5 : $textCount;
        if (lastTextCount != $textCount) {
            SetTextAreaHeight(element, maxHeight, minHeight);
        }
        if (document.activeElement == element) {
            setTimeout(function () { setTimeoutAutoHeight(element, maxHeight, minHeight, $textCount) }, 100);
        } else {
            SetTextAreaHeight(element, maxHeight, minHeight);
        }
    }
})(jQuery);
