/**
调用方式:
ZL_Diagram.InitDiagram方法初始化流程图；
参数说明:
id:页面容器id(该容器需要手动设定宽高值)
title:流程图标题
data:数据源，说明如下
数据格式:
以json数组格式存储，必须包含StepName字段用于用于显示步骤名称
**/
var ZL_Diagram = {
    makes: {},
    myDiagram:{},
    InitDiagram: function (id,title,data) {//初始化流程图方法，id:容器id(该容器需要固定高度),title:流程图标题,data:json数据
        _obj = this;
        _obj.makes = go.GraphObject.make;
        _obj.myDiagram = _obj.makes(go.Diagram, id,  // must name or refer to the DIV HTML element
                {
                    initialContentAlignment: go.Spot.Center,
                    allowDrop: true,  // must be true to accept drops from the Palette
                    "LinkDrawn": _obj.showLinkLabel,  // this DiagramEvent listener is defined below
                    "LinkRelinked": _obj.showLinkLabel,
                    "animationManager.duration": 800, // slightly longer than default (600ms) animation
                    "undoManager.isEnabled": true  // enable undo & redo
                });

        // when the document is modified, add a "*" to the title and enable the "Save" button
        _obj.myDiagram.addDiagramListener("Modified", function (e) {
            var button = document.getElementById("SaveButton");
            if (button) button.disabled = !myDiagram.isModified;
            var idx = document.title.indexOf("*");
            if (myDiagram.isModified) {
                if (idx < 0) document.title += "*";
            } else {
                if (idx >= 0) document.title = document.title.substr(0, idx);
            }
        });
        // define the Node templates for regular nodes
        var lightText = 'whitesmoke';

        _obj.myDiagram.nodeTemplateMap.add("",  // the default category
          _obj.makes(go.Node, "Spot", _obj.nodeStyle(),
            // the main object is a Panel that surrounds a TextBlock with a rectangular Shape
            _obj.makes(go.Panel, "Auto",
              _obj.makes(go.Shape, "Rectangle",
                { fill: "#00A9C9", stroke: null },
                new go.Binding("figure", "figure")),
              _obj.makes(go.TextBlock,
                {
                    font: "bold 11pt Helvetica, Arial, sans-serif",
                    stroke: lightText,
                    margin: 8,
                    maxSize: new go.Size(160, NaN),
                    wrap: go.TextBlock.WrapFit,
                    editable: true
                },
                new go.Binding("text", "text").makeTwoWay())
            ),
            // four named ports, one on each side:
           _obj.makePort("T", go.Spot.Top, false, true),
           _obj.makePort("L", go.Spot.Left, true, true),
           _obj.makePort("R", go.Spot.Right, true, true),
           _obj.makePort("B", go.Spot.Bottom, true, false)
          ));

        _obj.myDiagram.nodeTemplateMap.add("Start",
          _obj.makes(go.Node, "Spot", _obj.nodeStyle(),
            _obj.makes(go.Panel, "Auto",
              _obj.makes(go.Shape, "Circle",
                { minSize: new go.Size(40, 60), fill: "#79C900", stroke: null }),
              _obj.makes(go.TextBlock, "开始",
                { margin: 5, font: "bold 11pt Helvetica, Arial, sans-serif", stroke: lightText })
            ),
            // three named ports, one on each side except the top, all output only:
            _obj.makePort("L", go.Spot.Left, true, false),
            _obj.makePort("R", go.Spot.Right, true, false),
            _obj.makePort("B", go.Spot.Bottom, true, false)
          ));

        _obj.myDiagram.nodeTemplateMap.add("End",
          _obj.makes(go.Node, "Spot", _obj.nodeStyle(),
            _obj.makes(go.Panel, "Auto",
              _obj.makes(go.Shape, "Circle",
                { minSize: new go.Size(40, 60), fill: "#DC3C00", stroke: null }),
              _obj.makes(go.TextBlock, "结束",
                { margin: 5, font: "bold 11pt Helvetica, Arial, sans-serif", stroke: lightText })
            ),
            // three named ports, one on each side except the bottom, all input only:
            _obj.makePort("T", go.Spot.Top, false, true),
            _obj.makePort("L", go.Spot.Left, false, true),
            _obj.makePort("R", go.Spot.Right, false, true)
          ));

        _obj.myDiagram.nodeTemplateMap.add("Comment",
          _obj.makes(go.Node, "Auto", _obj.nodeStyle(),
            _obj.makes(go.Shape, "File",
              { fill: "#EFFAB4", stroke: null }),
            _obj.makes(go.TextBlock,
              {
                  margin: 5,
                  maxSize: new go.Size(200, NaN),
                  wrap: go.TextBlock.WrapFit,
                  textAlign: "center",
                  editable: true,
                  font: "bold 12pt Helvetica, Arial, sans-serif",
                  stroke: '#454545'
              },
              new go.Binding("text", "text").makeTwoWay())
            // no ports, because no links are allowed to connect with a comment
          ));

        // replace the default Link template in the linkTemplateMap
        _obj.myDiagram.linkTemplate =
          _obj.makes(go.Link,  // the whole link panel
            {
                routing: go.Link.AvoidsNodes,
                curve: go.Link.JumpOver,
                corner: 5, toShortLength: 4,
                relinkableFrom: true,
                relinkableTo: true,
                reshapable: true
            },
            new go.Binding("points").makeTwoWay(),
            _obj.makes(go.Shape,  // the link path shape
              { isPanelMain: true, stroke: "gray", strokeWidth: 2 }),
            _obj.makes(go.Shape,  // the arrowhead
              { toArrow: "standard", stroke: null, fill: "gray" }),
            _obj.makes(go.Panel, "Auto",  // the link label, normally not visible
              { visible: false, name: "LABEL", segmentIndex: 2, segmentFraction: 0.5 },
              new go.Binding("visible", "visible").makeTwoWay(),
              _obj.makes(go.Shape, "RoundedRectangle",  // the label shape
                { fill: "#F8F8F8", stroke: null }),
              _obj.makes(go.TextBlock, "Yes",  // the label
                {
                    textAlign: "center",
                    font: "10pt helvetica, arial, sans-serif",
                    stroke: "#333333",
                    editable: true
                },
                new go.Binding("text", "text").makeTwoWay())
            )
          );
        _obj.myDiagram.toolManager.linkingTool.temporaryLink.routing = go.Link.Orthogonal;
        _obj.myDiagram.toolManager.relinkingTool.temporaryLink.routing = go.Link.Orthogonal;
        //加载json方法
        _obj.CreateImg(title,data);
    },
    load: function (json) {
        this.myDiagram.model = go.Model.fromJson(json);
        console.log(this.myDiagram);
    },
    CreateImg: function (title,data) {
        imagedata = { "class": "go.GraphLinksModel", "linkFromPortIdProperty": "fromPort", "linkToPortIdProperty": "toPort", "nodeDataArray": [], "linkDataArray": [] };//初始化流程图数据
        var datas = data;//流程数据源
        console.log(datas);
        var proname = title;
        imagedata.nodeDataArray.push({ "category": "Comment", "loc": "360 -10", "text": proname, "key": -13 });
        imagedata.nodeDataArray.push({ "key": 0, "category": "Start", "loc": "175 0", "text": "用户提交" });
        var x = -150, y = 0;//初始位置
        for (var i = 0; i < datas.length; i++) {
            if (i % 6 == 0) {//6列换一行
                imagedata.linkDataArray.push({ "from": i, "to": i + 1, "fromPort": "B", "toPort": "T" });
                x = -150, y += 70;//换行初始化
            } else {
                imagedata.linkDataArray.push({ "from": i, "to": i + 1, "fromPort": "R", "toPort": "L" });
                x += 120
            }
            imagedata.nodeDataArray.push({ "key": i + 1, "loc": x + " " + y, "text": datas[i].StepName });
        }
        imagedata.nodeDataArray.push({ "key": -2, "category": "End", "loc": "175 210", "text": "结束" });
        imagedata.linkDataArray.push({ "from": datas.length, "to": -2, "fromPort": "B", "toPort": "T" });
        console.log(imagedata);
        this.load(JSON.stringify(imagedata));
    },
    showLinkLabel: function (e) {
        var label = e.subject.findObject("LABEL");
        if (label !== null) label.visible = (e.subject.fromNode.data.figure === "Diamond");
    },
    nodeStyle: function () {
        return [
              new go.Binding("location", "loc", go.Point.parse).makeTwoWay(go.Point.stringify),
              {
                  // the Node.location is at the center of each node
                  locationSpot: go.Spot.Center,
                  //isShadowed: true,
                  //shadowColor: "#888",
                  // handle mouse enter/leave events to show/hide the ports
                  mouseEnter: function (e, obj) { _obj.showPorts(obj.part, true); },
                  mouseLeave: function (e, obj) { _obj.showPorts(obj.part, false); }
              }
        ];
    },
    makePort: function (name, spot, output, input) {
        return _obj.makes(go.Shape, "Circle",
                     {
                         fill: "transparent",
                         stroke: null,  // this is changed to "white" in the showPorts function
                         desiredSize: new go.Size(8, 8),
                         alignment: spot, alignmentFocus: spot,  // align the port on the main Shape
                         portId: name,  // declare this object to be a "port"
                         fromSpot: spot, toSpot: spot,  // declare where links may connect at this port
                         fromLinkable: output, toLinkable: input,  // declare whether the user may draw links to/from here
                         cursor: "pointer"  // show a different cursor to indicate potential link point
                     });
    },
    showPorts: function (node, show) {
        var diagram = node.diagram;
        if (!diagram || diagram.isReadOnly || !diagram.allowLink) return;
        node.ports.each(function (port) {
            port.stroke = (show ? "white" : null);
        });
    }
}