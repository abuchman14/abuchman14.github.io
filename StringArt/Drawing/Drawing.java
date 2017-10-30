import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.*;
import java.awt.geom.*;
import java.util.*;

public class Drawing extends JFrame{
	
	JTextField tf;
	JLabel l;
	DrawPane d;
	
	public static void main(String[] args) {
		new Drawing();
		  
	}
	
	public Drawing()
	{	
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        
          d = new DrawPane();
		  setContentPane(d);   
		  setSize(400,400);  
		  setLayout(null);  
		  setVisible(true);
        
		  setSize(800, 800);
		  setVisible(true); 
	}
	
	class DrawPane extends JPanel{
		int numPoints = 100;
		int skip = 50;
		
        public DrawPane() {
		}

		public void paintComponent(Graphics g){
			System.out.println(numPoints + " " + skip);
        	Color black = new Color(0,0,0);
        	Color white = new Color(255, 255, 255);
        	g.fillRect(0, 0, 800, 800);
        	
        	g.setColor(white);
        	
        	int cx = 400;
        	int cy = 400;
        	int R = 200;
        	int visitedCount = 0;        
        
    		ArrayList<Point> dots = new ArrayList<Point>();

           for (int i=0; i<numPoints; i++)
           {
        	   int px = (int)(R*Math.cos(i * 2*Math.PI / numPoints)) + cx;
        	   int py = (int)(R*Math.sin(i * 2*Math.PI / numPoints)) + cy;
        	   
        	   Point p = new Point(px, py);
        	   dots.add(p);
        	   g.drawOval(px, py, 1, 1);	   
           }
  
        int i = 0;
        Point p1 = dots.get(0);
		while (visitedCount < dots.size())
		{
			i = i + skip;
			Point p2 = dots.get(i % dots.size());

			
			if (p2.visited == false) {
				p2.visited = true;
				visitedCount++;


				g.drawLine(p1.x, p1.y, p2.x, p2.y);

				p1 = p2;
			}

			else {
				i = i + 1;
				p1 = dots.get(i%dots.size());
			}

		}

	}
           
	}

}

