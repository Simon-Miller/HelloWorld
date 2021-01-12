var canvasBar;
(function (canvasBar) {
    var bar = /** @class */ (function () {
        function bar(canvasWidth, canvasHeight, topColour, bottomColour, 
        /** The HtmlElement reference passed by Blazor for us to render a Canvas into. */
        element, 
        /** .NET assembly that owns the component that initialized this class. */
        hub) {
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;
            this.topColour = topColour;
            this.bottomColour = bottomColour;
            this.element = element;
            this.hub = hub;
            this.hasCanvas = false;
            this.canvas = null;
        }
        bar.prototype.onClicked = function (e) {
            debugger;
            this.hub.invokeMethodAsync("clicked", e.offsetX, e.offsetY); // IS IT REALLY THAT EASY??
        };
        bar.register = function (hub, element, canvasWidth, canvasHeight, topColour, bottomColour) {
            var detail = new bar(canvasWidth, canvasHeight, topColour, bottomColour, element, hub);
            this.registrations.push(detail);
            // I suppose we should render it to screen.
            bar.render(element, detail);
        };
        bar.render = function (element, details) {
            if (details.hasCanvas == false) {
                element.innerHTML = "<canvas width=\"" + details.canvasWidth + "\" height=\"" + details.canvasHeight + "\" />";
                var canvas = element.firstChild;
                canvas.width = details.canvasWidth;
                canvas.height = details.canvasHeight;
                element.appendChild(canvas);
                details.hasCanvas = true;
                details.canvas = canvas;
                element.addEventListener("click", function (e) { return details.onClicked(e); });
            }
            var ctx = details.canvas.getContext("2d");
            var gradient = ctx.createLinearGradient(0, 0, 0, details.canvasHeight);
            gradient.addColorStop(0, details.topColour);
            gradient.addColorStop(1.0, details.bottomColour);
            ctx.fillStyle = gradient;
            ctx.fillRect(0, 0, details.canvasWidth, details.canvasHeight);
        };
        //#region static stuff
        bar.registrations = [];
        return bar;
    }());
    canvasBar.bar = bar;
})(canvasBar || (canvasBar = {}));
//# sourceMappingURL=canvasBar.js.map