namespace Fractals

open System
open System.Numerics
open System.DrawingCore

module public Mandelbrot = 

    let mandelbrot_func (c:Complex) (z:Complex) = z*z+c

    let rec mandelbrot_iterations m n c x =
        if n=m then (Complex.Abs(x)<1.0, n)
        else 
            let y = mandelbrot_func c x
            if Complex.Abs(y)>2.0 then (false, n) 
            else 
                let it = mandelbrot_iterations m (n+1) c y
                it

    let scale (x:float,y:float) (u,v) n = float(n-u)/float(v-u)*(y-x)+x

    let color k = 
        match k with
        | 0 -> Color.White
        | 1 -> Color.GreenYellow
        | 2 -> Color.LawnGreen
        | 3 -> Color.YellowGreen
        | 4 -> Color.MediumSeaGreen
        | 5 -> Color.MediumSlateBlue
        | 6 -> Color.MediumOrchid
        | 7 -> Color.LightCoral
        | 8 -> Color.Fuchsia
        | 9 -> Color.Crimson
        | 10 -> Color.DarkMagenta
        | _ -> Color.DarkViolet

    let picture (p:int, s:float, it:int) =
        let image = new Bitmap (p, p)
        let lscale = scale (-s,s) (0,image.Height-1)
        for i = 0 to (image.Height-1) do
          for j = 0 to (image.Width-1) do
            let t = Complex (lscale i, lscale j)
            let mandelres = (mandelbrot_iterations it 0 t Complex.Zero)
            image.SetPixel(i,j, if fst mandelres then Color.Black else color (snd mandelres))
        image
        
;;

//[<EntryPoint>]
//let main argv =
//    let image = picture(800, 2.0, 100)
//    image.Save("mandelbrot.png")
//    0;; // return an integer exit code