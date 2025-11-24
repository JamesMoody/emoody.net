
window.onload = function () {
    startStarfield("activebackdrop", 100);
};

function startStarfield(targetElementId, numOfStarts) {
    var starField = new StarField(targetElementId, numOfStarts);

    // setup a main (game) loop
    // note: this could be encapsulated inside the starfield class via MainLoop.createInstance() using MultiMain @ https://gist.github.com/IceCreamYou/6b16d8350bc4125a08c76b499e0d4c59
    // chose highly visible, single loop cuz hiding any form of multi-tasking is a bad idea.
    MainLoop
        .setMaxAllowedFPS(33) // no need to kill the CPU for a background effect
        .setUpdate( function () { starField.move(); } ) // the update event moves the stars        
        .setDraw(   function () { starField.draw(); } ) // the draw event (naturally) draws the stars
        .start();
}


// represents a single star
class Star {
    constructor() {
        // prep misc values
        this.maxDepth   = 32;
        this.starColors = [
            {color: "90a6ff"}, //144 166 255
            {color: "c3d1ff"}, //195 209 255
            {color: "ccd8ff"}, //204 216 255
            {color: "ffe3b2"}, //255 227 178
            {color: "ffc168"}, //255 193 104
            {color: "ffab34"}, //255 171 052
            {color: "ff9d00"}  //255 157 000
        ];

        // set the start at a random location, color, and depth
        this.x         = this.randomRange(-25,25);
        this.y         = this.randomRange(-25,25);
        this.z         = this.randomRange(1,this.maxDepth);
        var colorIndex = this.randomRange(0,this.starColors.length+1);
        this.color     = this.starColors[colorIndex].color;
    }

    randomRange(minVal,maxVal) {
        // quickie to get a random value...
        var ret = Math.floor(Math.random() * (maxVal - minVal - 1)) + minVal;
        if (ret === 0) { ret = 1; } // an xy of 0x0 looks strange... never have it.
        return ret;
    }

    recycle() {
        // be kind... recycle the star rather than just dumping (& recreating) the object
        var colorIndex = this.randomRange(0,this.starColors.length+1);
        this.x         = this.randomRange(-25,25);
        this.y         = this.randomRange(-25,25);
        this.z         = this.maxDepth;              // set max (not random) depth
        this.color     = this.starColors[colorIndex].color;
    }
}

// represents a starfield (that's drawn on a canvas).
class StarField {
    constructor(targetID, maxNumStars) {
        // prep misc values
        this.targetElementId = targetID;
        this.targetEl        = document.getElementById(targetID);
        this.targetContext   = this.targetEl.getContext("2d");
        this.numStars        = maxNumStars;
        this.stars           = new Array(this.numStars);
      
        // prep the star array
        for( var i = 0; i < this.stars.length; i++ ) {
            this.stars[i] = new Star();
        }
    }
 
   move() {
        // move the star along the z axis
        for( var i = 0; i < this.stars.length; i++ ) {
            this.stars[i].z -= 0.2;
 
            // recycle the star if it moved "off the screen"
            if( this.stars[i].z <= 0 ) {
                this.stars[i].recycle();
            }
        }
   }
   
    draw() {
        var fullWidth  = this.targetEl.width;
        var fullHeight = this.targetEl.height;
        var halfWidth  = this.targetEl.width / 2;
        var halfHeight = this.targetEl.height / 2;
        var sizeScale  = 1;
 
        // clear/reset the canvas
        this.targetContext.fillStyle = "rgb(0,0,0)";
        this.targetContext.fillRect(0,0,fullWidth,fullHeight);
      
        // calc the screen position of each star
        for( var i = 0; i < this.stars.length; i++ ) {
            var k  = 128.0 / this.stars[i].z;
            var px = this.stars[i].x * k + halfWidth;
            var py = this.stars[i].y * k + halfHeight;

            // only draw what's on the screen
            if( px >= 0 && px <= fullWidth && py >= 0 && py <= fullHeight ) {
                var size  = (1 - this.stars[i].z / 32.0) * sizeScale;
                var shade = parseInt((1 - this.stars[i].z / 32.0) * 255);

                // set the star's color -- todo: fade in
                this.targetContext.fillStyle = "#" + this.stars[i].color;

                // draw the star via simple circle
                this.targetContext.beginPath();
                this.targetContext.arc(px,py, size, 0, 360);
                this.targetContext.closePath();
                this.targetContext.fill();
            }
        }
    }
}