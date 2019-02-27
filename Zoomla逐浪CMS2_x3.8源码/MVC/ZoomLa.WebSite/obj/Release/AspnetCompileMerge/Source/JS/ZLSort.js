var ZLSort =
    {
        init: function (ids) {//参数sortul1,sortul2
            this.idArr = ids.split(',');//如果需要使用参数,达里引入
            for (var i = 0; i < this.idArr.length; i++) {
                $("#" + this.idArr[i]).sortable();
                this.orginSortArr.push({ "id": this.idArr[i], "arr": this.GetCurArr(this.idArr[i]) });
            }
        },
        GetCurArr: function (id) {//将数据压入数组
            var Arr = new Array();
            $("#" + id + " input[type='hidden']").each(function () {
                var tid = $(this).val().split(':')[0];
                var oid = $(this).val().split(':')[1];
                Arr.push({ "id": tid, "oid": oid });
            });
            return Arr;
        },
        UpdateArr: function () {//更新Orgin排序,OrderID不变，更新ID值
            for (var i = 0; i < this.idArr.length; i++) {//获取现在的排序位置
                var nowArr = this.GetCurArr(this.idArr[i]);
                for (var j = 0; j < this.orginSortArr[i].arr.length; j++) {//更新数组中排序的位置
                    this.orginSortArr[i].arr[j].id = nowArr[j].id;
                }
            }
            return this.orginSortArr;
        },
        GetArrByID: function (id) {
            for (var i = 0; i < this.orginSortArr.length; i++) {
                if (this.orginSortArr[i].id == id) { return this.orginSortArr[i].arr; }
            }
        },
        PostSort: function (id) {//需要更新的ul或table
            var a = "TableOrder";
            var i = $("#" + id).attr("data-tname");
            this.UpdateArr();
            var v = this.GetArrByID(id);//控件的ID，sortul等
            $.ajax({
                type: "Post",
                url: "/API/JSServe.ashx",
                data: { action: a, value: JSON.stringify(v), info: i },
                success: function () {
                    location = location;
                },
                error: function () { }
            })
        },
        idArr: new Array(),
        orginSortArr: new Array()//用于存放原始排序
    }