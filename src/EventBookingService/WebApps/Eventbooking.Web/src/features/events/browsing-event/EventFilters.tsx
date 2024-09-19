import React from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Checkbox } from "@/components/ui/checkbox";
import { FilterIcon } from "lucide-react";

const EventFilters: React.FC = () => {
  return (
    <div className="border rounded-lg p-4">
      <h2 className="text-xl font-semibold mb-4">Filters</h2>
      <div className="space-y-4">
        <div>
          <label htmlFor="date" className="block text-sm font-medium text-gray-700 mb-1">
            Date
          </label>
          <Input type="date" id="date" className="w-full" />
        </div>
        <div>
          <label htmlFor="category" className="block text-sm font-medium text-gray-700 mb-1">
            Category
          </label>
          <Select>
            <SelectTrigger id="category">
              <SelectValue placeholder="Select category" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="music">Music</SelectItem>
              <SelectItem value="sports">Sports</SelectItem>
              <SelectItem value="arts">Arts</SelectItem>
              <SelectItem value="food">Food</SelectItem>
            </SelectContent>
          </Select>
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Price Range
          </label>
          <div className="flex items-center space-x-2">
            <Input type="number" placeholder="Min" className="w-1/2" />
            <span>-</span>
            <Input type="number" placeholder="Max" className="w-1/2" />
          </div>
        </div>
        <div className="space-y-2">
          <label className="block text-sm font-medium text-gray-700">
            Features
          </label>
          <div className="flex items-center space-x-2">
            <Checkbox id="virtual" />
            <label htmlFor="virtual" className="text-sm text-gray-700">Virtual Event</label>
          </div>
          <div className="flex items-center space-x-2">
            <Checkbox id="accessible" />
            <label htmlFor="accessible" className="text-sm text-gray-700">Accessible</label>
          </div>
        </div>
        <Button className="w-full">
          <FilterIcon className="mr-2 h-4 w-4" />
          Apply Filters
        </Button>
      </div>
    </div>
  );
};

export default EventFilters;