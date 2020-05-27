(function($){
  var methods = {

    init: function(options) {
      return this.each(function(){
        var $this = $(this),
            data = $this.data('eraser');

        if (!data) {

          var handleImage = function() {
            var width = $this.width(),
                height = $this.height(),
                pos = $this.offset(),
                $canvas = $('<canvas/>'),
                canvas = $canvas.get(0),
                size = (options && options.size) ? options.size : 40,
                completeRatio = (options && options.completeRatio) ? options.completeRatio : .7,
                completeFunction = (options && options.completeFunction) ? options.completeFunction : null,
                progressFunction = (options && options.progressFunction) ? options.progressFunction : null,
                zIndex = $this.css('z-index') == "auto"?1:$this.css('z-index'),
                parts = [],
                colParts = Math.floor(width / size),
                numParts = colParts * Math.floor(height / size),
                n = numParts,
                opacity = (options && options.opacity) ? options.opacity : 0
                ctx = canvas.getContext('2d'),
                that = $this[0];

            // replace target with canvas
            $this.after($canvas);
            canvas.id = that.id;
            canvas.className = that.className;
            canvas.width = width;
            canvas.height = height;
              
            ctx.globalAlpha = 1 - parseFloat(opacity).toFixed(2);// 0.70;
            ctx.drawImage(that, 0, 0);
            $this.remove();

            // prepare context for drawing operations
            ctx.globalCompositeOperation = 'destination-out';
            ctx.strokeStyle = 'rgba(255,0,0,255)';
            ctx.lineWidth = size;

            ctx.lineCap = 'round';
            // bind events
            $canvas.bind('mousedown.eraser', methods.mouseDown);
            $canvas.bind('touchstart.eraser', methods.touchStart);
            $canvas.bind('touchmove.eraser', methods.touchMove);
            $canvas.bind('touchend.eraser', methods.touchEnd);

            // reset parts
            while(n--) parts.push(1);

            // store values
            data = {
              posX: pos.left,
              posY: pos.top,
              touchDown: false,
              touchID: -999,
              touchX: 0,
              touchY: 0,
              ptouchX: 0,
              ptouchY: 0,
              canvas: $canvas,
              ctx: ctx,
              w: width,
              h: height,
              source: that,
              size: size,
              parts: parts,
              colParts: colParts,
              numParts: numParts,
              ratio: 0,
              complete: false,
              completeRatio: completeRatio,
              completeFunction: completeFunction,
              progressFunction: progressFunction,
              zIndex: zIndex
            };
            $canvas.data('eraser', data);

            // listen for resize event to update offset values
            $(window).resize(function() {
              var pos = $canvas.offset();
              data.posX = pos.left;
              data.posY = pos.top;
            });
          }

          if (this.complete && this.naturalWidth > 0) {
            handleImage();
          } else {
            //this.onload = handleImage;
            $this.load(handleImage);
          }
        }
      });
    },

    touchStart: function(event) {
      var $this = $(this),
          data = $this.data('eraser');

      if (!data.touchDown) {
        var t = event.originalEvent.changedTouches[0],
            tx = t.pageX - data.posX,
            ty = t.pageY - data.posY;
        methods.evaluatePoint(data, tx, ty);
        data.touchDown = true;
        data.touchID = t.identifier;
        data.touchX = tx;
        data.touchY = ty;
        event.preventDefault();
      }
    },

    touchMove: function(event) {
      var $this = $(this),
          data = $this.data('eraser');

      if (data.touchDown) {
        var ta = event.originalEvent.changedTouches,
            n = ta.length;
        while (n--) {
          if (ta[n].identifier == data.touchID) {
            var tx = ta[n].pageX - data.posX,
                ty = ta[n].pageY - data.posY;
            methods.evaluatePoint(data, tx, ty);
            data.ctx.beginPath();
            data.ctx.moveTo(data.touchX, data.touchY);
            data.touchX = tx;
            data.touchY = ty;
            data.ctx.lineTo(data.touchX, data.touchY);
            data.ctx.stroke();
            $this.css({"z-index":$this.css('z-index')==data.zIndex?parseInt(data.zIndex)+1:data.zIndex});
            event.preventDefault();
            break;
          }
        }
      }
    },

    touchEnd: function(event) {
      var $this = $(this),
        data = $this.data('eraser');

      if ( data.touchDown ) {
        var ta = event.originalEvent.changedTouches,
          n = ta.length;
        while( n-- ) {
          if ( ta[n].identifier == data.touchID ) {
            data.touchDown = false;
            event.preventDefault();
            break;
          }
        }
      }
    },

    evaluatePoint: function(data, tx, ty) {
      var p = Math.floor(tx/data.size) + Math.floor( ty / data.size ) * data.colParts;

      if ( p >= 0 && p < data.numParts ) {
        data.ratio += data.parts[p];
        data.parts[p] = 0;
        if (!data.complete) {
          p = data.ratio/data.numParts;
          if ( p >= data.completeRatio ) {
            data.complete = true;
            if ( data.completeFunction != null ) data.completeFunction();
          } else {
            if ( data.progressFunction != null ) data.progressFunction(p);
          }
        }
      }

    },

    mouseDown: function(event) {
      var $this = $(this),
          data = $this.data('eraser'),
          tx = event.pageX - data.posX,
          ty = event.pageY - data.posY;

      methods.evaluatePoint( data, tx, ty );
      data.touchDown = true;
      data.touchX = tx;
      data.touchY = ty;
      data.ctx.beginPath();
      data.ctx.moveTo(data.touchX-1, data.touchY);
      data.ctx.lineTo(data.touchX, data.touchY);
      data.ctx.stroke();
      $this.bind('mousemove.eraser', methods.mouseMove);
      $(document).bind('mouseup.eraser', data, methods.mouseUp);
      event.preventDefault();
    },

    mouseMove: function(event) {
      var $this = $(this),
          data = $this.data('eraser'),
          tx = event.pageX - data.posX,
          ty = event.pageY - data.posY;

      methods.evaluatePoint( data, tx, ty );
      data.ctx.beginPath();
      data.ctx.moveTo( data.touchX, data.touchY );
      data.touchX = tx;
      data.touchY = ty;
      data.ctx.lineTo( data.touchX, data.touchY );
      data.ctx.stroke();
      $this.css({"z-index":$this.css('z-index')==data.zIndex?parseInt(data.zIndex)+1:data.zIndex});
      event.preventDefault();
    },

    mouseUp: function(event) {
      var data = event.data,
          $this = data.canvas;

      data.touchDown = false;
      $this.unbind('mousemove.eraser');
      $(document).unbind('mouseup.eraser');
      event.preventDefault();
    },

    clear: function() {
      var $this = $(this),
          data = $this.data('eraser');

      if (data) {
        data.ctx.clearRect(0, 0, data.w, data.h);
        var n = data.numParts;
        while(n--) data.parts[n] = 0;
        data.ratio = data.numParts;
        data.complete = true;
        if (data.completeFunction != null) data.completeFunction();
      }
    },

    size: function(value) {
      var $this = $(this),
          data = $this.data('eraser');

      if (data && value) {
        data.size = value;
        data.ctx.lineWidth = value;
      }
    },

    reset: function() {
      var $this = $(this),
          data = $this.data('eraser');

      if (data) {
        data.ctx.globalCompositeOperation = 'source-over';
        data.ctx.drawImage( data.source, 0, 0 );
        data.ctx.globalCompositeOperation = 'destination-out';
        var n = data.numParts;
        while (n--) data.parts[n] = 1;
        data.ratio = 0;
        data.complete = false;
        data.touchDown = false;
      }
    },

    progress: function() {
      var $this = $(this),
          data = $this.data('eraser');

      if (data) {
        return data.ratio/data.numParts;
      }
      return 0;
    }

  };

  $.fn.eraser = function(method) {
    if (methods[method]) {
      return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
    } else if (typeof method === 'object' || ! method) {
      return methods.init.apply(this, arguments);
    } else {
      $.error('Method ' +  method + ' does not yet exist on jQuery.eraser');
    }
  };
})(jQuery);
