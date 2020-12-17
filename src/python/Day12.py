import math

class Day12():
    def parse(self):
        lines = []
        with open ('input12') as f:
            lines = f.readlines()
        return lines

    def part1(self):
        lines = self.parse()
        east = 0; north = 0
        
        # Initial direction - East
        direction = (1, 0) # (x, y) 

        for l in lines:
            action = l[0]
            value = int(l[1:])

            if action == 'N':
                north += value
            elif action == 'S':
                north -= value
            elif action == 'E':
                east += value
            elif action == 'W':
                east -= value
            elif action == 'L':
                direction = self.turnLeft(direction, value)
            elif action == 'R':
                direction = self.turnRight(direction, value)
            elif action == 'F':
                north = north + (direction[1] * value)
                east = east + (direction[0] * value)
            
            # print(f'{l}: ({east}, {north})')
        
        return math.fabs(east) + math.fabs(north)

    def turnLeft(self, currentDir, value):
        x = currentDir[0]
        y = currentDir[1]

        if value == 180:
            return (-x, -y)
        elif value == 90:
            if currentDir == (1,0):
                return (0,1)
            elif currentDir == (0,1):
                return (-1,0)
            elif currentDir == (-1,0):
                return (0,-1)
            elif currentDir == (0,-1):
                return (1,0)
        elif value == 270:
            return self.turnRight(currentDir, 90)
        else:
            raise Exception

    def turnRight(self, currentDir, value):
        x = currentDir[0]
        y = currentDir[1]

        if value == 180:
            return (-x, -y)
        elif value == 90:
            if currentDir == (1,0):
                return (0,-1)
            elif currentDir == (0,-1):
                return (-1,0)
            elif currentDir == (-1,0):
                return (0,1)
            elif currentDir == (0,1):
                return (1,0)
        elif value == 270:
            return self.turnLeft(currentDir, 90)
        else:
            raise Exception

if __name__ == "__main__":
    d12 = Day12().part1()
    print(d12)