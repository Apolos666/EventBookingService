import React, { useState } from 'react';
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import { Label } from "@/components/ui/label";
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle, DialogTrigger, DialogFooter } from "@/components/ui/dialog";
import { PlusIcon, XIcon, ImageIcon } from "lucide-react";
import { Location } from "./add-event.types";

const AddEventDialog: React.FC = () => {
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [locations, setLocations] = useState<Location[]>([{ name: '', price: '' }]);
  const [image, setImage] = useState<File | null>(null);

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    const eventData = {
      name: formData.get('name'),
      description: formData.get('description'),
      startDate: formData.get('startDate'),
      endDate: formData.get('endDate'),
      longDescription: formData.get('longDescription'),
      locations: locations,
      image: image
    };
    console.log(eventData);
    setIsDialogOpen(false);
  };

  const addLocation = () => {
    setLocations([...locations, { name: '', price: '' }]);
  };

  const removeLocation = (index: number) => {
    setLocations(locations.filter((_, i) => i !== index));
  };

  const updateLocation = (index: number, field: keyof Location, value: string) => {
    const newLocations = [...locations];
    newLocations[index][field] = value;
    setLocations(newLocations);
  };

  const handleImageChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files && event.target.files[0]) {
      setImage(event.target.files[0]);
    }
  };

  return (
    <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
      <DialogTrigger asChild>
        <Button variant="outline" className="hidden md:flex">
          <PlusIcon className="mr-2 h-4 w-4" />
          Add Event
        </Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[600px]">
        <DialogHeader>
          <DialogTitle>Add New Event</DialogTitle>
          <DialogDescription>
            Fill in the details of your new event. Click save when you're done.
          </DialogDescription>
        </DialogHeader>
        <form onSubmit={handleSubmit}>
          <div className="grid gap-4 py-4">
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="name" className="text-right">
                Event Name
              </Label>
              <Input id="name" name="name" className="col-span-3" required />
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="description" className="text-right">
                Short Description
              </Label>
              <Textarea id="description" name="description" className="col-span-3" required />
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="startDate" className="text-right">
                Start Date
              </Label>
              <Input id="startDate" name="startDate" type="datetime-local" className="col-span-3" required />
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="endDate" className="text-right">
                End Date
              </Label>
              <Input id="endDate" name="endDate" type="datetime-local" className="col-span-3" required />
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="longDescription" className="text-right">
                Long Description
              </Label>
              <Textarea id="longDescription" name="longDescription" className="col-span-3" rows={4} required />
            </div>
            <div className="grid grid-cols-4 items-start gap-4">
              <Label className="text-right mt-2">
                Locations & Prices
              </Label>
              <div className="col-span-3 space-y-2">
                {locations.map((location, index) => (
                  <div key={index} className="flex items-center space-x-2">
                    <Input
                      placeholder="Location name"
                      value={location.name}
                      onChange={(e) => updateLocation(index, 'name', e.target.value)}
                      required
                    />
                    <Input
                      placeholder="Price"
                      value={location.price}
                      onChange={(e) => updateLocation(index, 'price', e.target.value)}
                      required
                    />
                    <Button type="button" variant="outline" size="icon" onClick={() => removeLocation(index)}>
                      <XIcon className="h-4 w-4" />
                    </Button>
                  </div>
                ))}
                <Button type="button" variant="outline" onClick={addLocation}>
                  Add Location
                </Button>
              </div>
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="image" className="text-right">
                Event Image
              </Label>
              <div className="col-span-3">
                <Input id="image" name="image" type="file" accept="image/*" onChange={handleImageChange} />
                {image && (
                  <div className="mt-2 flex items-center space-x-2">
                    <ImageIcon className="h-5 w-5" />
                    <span className="text-sm">{image.name}</span>
                  </div>
                )}
              </div>
            </div>
          </div>
          <DialogFooter>
            <Button type="submit">Save Event</Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
};

export default AddEventDialog;