
namespace canvasBar {

    interface iHub extends DotNet.DotNetObject {

    }

    export class bar {

        constructor(
            public canvasWidth: number,
            public canvasHeight: number,
            public topColour: string,
            public bottomColour: string,

            /** The HtmlElement reference passed by Blazor for us to render a Canvas into. */
            private readonly element: HTMLElement,

            /** .NET assembly that owns the component that initialized this class. */
            private readonly hub: iHub
        ) { }

        public hasCanvas: boolean = false;
        public canvas: HTMLCanvasElement = null;


        public onClicked(e: MouseEvent): void {
            debugger;
            this.hub.invokeMethodAsync("clicked", e.offsetX, e.offsetY); // IS IT REALLY THAT EASY??
        }


        //#region static stuff

        private static registrations: Array<bar> = [];

        public static register(hub: iHub, element: HTMLElement, canvasWidth: number, canvasHeight: number, topColour: string, bottomColour: string): void {
            var detail = new bar(canvasWidth, canvasHeight, topColour, bottomColour, element, hub);
            this.registrations.push(detail);

            // I suppose we should render it to screen.
            bar.render(element, detail);
        }

        private static render(element: HTMLElement, details: bar) {
            if (details.hasCanvas == false) {
                element.innerHTML = `<canvas width="${details.canvasWidth}" height="${details.canvasHeight}" />`;
                var canvas = element.firstChild as HTMLCanvasElement;
                canvas.width = details.canvasWidth;
                canvas.height = details.canvasHeight;
                element.appendChild(canvas);

                details.hasCanvas = true;
                details.canvas = canvas;

                element.addEventListener("click", e => details.onClicked(e));
            }

            var ctx = details.canvas.getContext("2d");

            var gradient = ctx.createLinearGradient(0, 0, 0, details.canvasHeight);
            gradient.addColorStop(0, details.topColour);
            gradient.addColorStop(1.0, details.bottomColour);
            ctx.fillStyle = gradient;
            ctx.fillRect(0, 0, details.canvasWidth, details.canvasHeight);
        }

        //#endregion
    }
}